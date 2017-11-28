package ip.edu.uj.wifitracker;

import android.Manifest;
import android.app.Activity;
import android.app.NotificationManager;
import android.app.PendingIntent;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.pm.PackageManager;
import android.os.Bundle;
import android.provider.Settings;
import android.support.v4.app.ActivityCompat;
import android.support.v4.app.NotificationCompat;
import android.support.v4.content.ContextCompat;
import android.util.Log;
import android.view.View;
import android.widget.ToggleButton;

import com.loopj.android.http.AsyncHttpResponseHandler;
import com.loopj.android.http.RequestParams;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.security.Security;
import java.util.HashMap;
import java.util.Map;

import cz.msebera.android.httpclient.Header;

public class MainActivity extends Activity {

    private static final Map<String, Integer> REQUESTED_PERMISSIONS = new HashMap<>();
    private ToggleButton toggleButton;
    private static final int NOTIFICATION_ID = 123;
    private SharedPreferences sharedPrefs;

    static {
        REQUESTED_PERMISSIONS.put(Manifest.permission.ACCESS_FINE_LOCATION, 0);
        REQUESTED_PERMISSIONS.put(Manifest.permission.CHANGE_WIFI_STATE, 1);
        REQUESTED_PERMISSIONS.put(Manifest.permission.ACCESS_WIFI_STATE, 2);
        REQUESTED_PERMISSIONS.put(Manifest.permission.ACCESS_COARSE_LOCATION, 3);
        REQUESTED_PERMISSIONS.put(Manifest.permission.INTERNET, 4);
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        for (Map.Entry<String, Integer> entry : REQUESTED_PERMISSIONS.entrySet()) {
            requestPermission(entry.getKey(), entry.getValue());
        }

        toggleButton = findViewById(R.id.toggleTrackingButton);

        sharedPrefs = getSharedPreferences(
                getString(R.string.sharedPrefsName), Context.MODE_PRIVATE);

        toggleButton.setChecked(sharedPrefs.getBoolean(getString(R.string.appRunning), false));
        BackgroundTrackerService.DEVICE_ID = Settings.Secure.getString(getApplicationContext().getContentResolver(),
                Settings.Secure.ANDROID_ID);
    }

    @Override
    protected void onDestroy() {
        super.onDestroy();
    }

    public void trackingClicked(View view) {

        Log.i(getString(R.string.AppIdentificator), "trackingClicked");


        if(false){
            return;
        }

        SharedPreferences.Editor editor = sharedPrefs.edit();

        if (toggleButton.isChecked()) {
            LocationTracker.getInstance(getApplicationContext()).startUpdates();

            Intent notificationIntent = new Intent(this, MainActivity.class);
            PendingIntent pendingIntent =
                    PendingIntent.getActivity(this, 0, notificationIntent, 0);

            NotificationManager mNotifyMgr =
                    (NotificationManager) getSystemService(NOTIFICATION_SERVICE);
            mNotifyMgr.notify(NOTIFICATION_ID, new NotificationCompat.Builder(this, "channelID")
                    .setSmallIcon(R.drawable.sending_icon)
                    .setOngoing(true)
                    .setContentTitle("Wi-Fi Tracker")
                    .setContentText("Tracking wireless networks...")
                    .setContentIntent(pendingIntent)
                    .build());

            editor.putBoolean(getString(R.string.appRunning), true);
            editor.apply();
            startService(new Intent(this, BackgroundTrackerService.class).setAction("TRACK"));
        } else {
            ((NotificationManager) getSystemService(NOTIFICATION_SERVICE)).cancel(NOTIFICATION_ID);
            editor.putBoolean(getString(R.string.appRunning), false);
            editor.apply();
            stopService(new Intent(this, BackgroundTrackerService.class));
            BackgroundTrackerService.run.set(false);
            LocationTracker.getInstance(getApplicationContext()).stopUpdates();

        }
    }

    public void requestPermission(String permission, int requestCode) {
        if (ContextCompat.checkSelfPermission(this,
                permission)
                != PackageManager.PERMISSION_GRANTED) {


            ActivityCompat.requestPermissions(this,
                    new String[]{permission},
                    requestCode);
        }
    }
}
