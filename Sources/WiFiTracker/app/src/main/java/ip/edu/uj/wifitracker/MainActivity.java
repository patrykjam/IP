package ip.edu.uj.wifitracker;

import android.Manifest;
import android.app.Activity;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.content.pm.PackageManager;
import android.net.wifi.ScanResult;
import android.net.wifi.WifiManager;
import android.os.Bundle;
import android.provider.Settings;
import android.support.v4.app.ActivityCompat;
import android.support.v4.content.ContextCompat;
import android.view.View;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class MainActivity extends Activity {

    List<ScanResult> results;
    WifiManager wifiManager;
    private static final Map<String, Integer> REQUESTED_PERMISSIONS = new HashMap<>();

    static {
        REQUESTED_PERMISSIONS.put(Manifest.permission.ACCESS_FINE_LOCATION, 0);
        REQUESTED_PERMISSIONS.put(Manifest.permission.CHANGE_WIFI_STATE, 1);
        REQUESTED_PERMISSIONS.put(Manifest.permission.ACCESS_WIFI_STATE, 2);
        REQUESTED_PERMISSIONS.put(Manifest.permission.ACCESS_COARSE_LOCATION,3);
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        for (Map.Entry<String, Integer> entry : REQUESTED_PERMISSIONS.entrySet()) {
            requestPermission(entry.getKey(), entry.getValue());
        }

        String myAndroidDeviceId = Settings.Secure.getString(getApplicationContext().getContentResolver(), Settings.Secure.ANDROID_ID);


        wifiManager = (WifiManager) getApplicationContext().getSystemService(WIFI_SERVICE);
        registerReceiver(new BroadcastReceiver() {
            @Override
            public void onReceive(Context c, Intent intent) {
                results = wifiManager.getScanResults();
            }
        }, new IntentFilter(WifiManager.SCAN_RESULTS_AVAILABLE_ACTION));
    }

    public void trackingClicked(View view) {
        wifiManager.startScan();
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
