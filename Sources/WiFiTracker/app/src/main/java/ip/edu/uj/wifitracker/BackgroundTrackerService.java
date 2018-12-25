package ip.edu.uj.wifitracker;

import android.app.IntentService;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.net.wifi.ScanResult;
import android.util.Log;

import com.loopj.android.http.AsyncHttpResponseHandler;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.UnsupportedEncodingException;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.List;
import java.util.concurrent.atomic.AtomicBoolean;

import cz.msebera.android.httpclient.Header;

public class BackgroundTrackerService extends IntentService {

    private WiFiNetworkTracker wiFiNetworkTracker;
    public static String DEVICE_ID = "";
    private boolean running = false;
    public static AtomicBoolean run = new AtomicBoolean(true);

    public BackgroundTrackerService() {
        super("BackgroundTrackerService");
    }

    @Override
    protected void onHandleIntent(Intent intent) {
        if (intent != null && intent.getAction() != null && intent.getAction().equals("TRACK")) {
            wiFiNetworkTracker = new WiFiNetworkTracker(getApplicationContext(), onScanResultsReceiver);
            running = true;
            run.set(true);
            new Thread(new Runnable() {
                @Override
                public void run() {
                    while (run.get()) {
                        try {
                            wiFiNetworkTracker.startScan();
                            Thread.sleep(15000);
                        } catch (InterruptedException e) {
                            Thread.currentThread().interrupt();
                        }
                    }
                    LocationTracker.getInstance(getApplicationContext()).stopUpdates();
                }
            }).start();
        }
    }

    @Override
    public void onDestroy() {
        Log.i(getString(R.string.AppIdentificator), "Service onDestroy");
//        if(running) {
//            unregisterReceiver(onScanResultsReceiver);
//            Log.i(getString(R.string.AppIdentificator), "Unregistered receiver");
//        }
        running = false;
//        LocationTracker.getInstance(getApplicationContext()).stopUpdates();
        super.onDestroy();
    }

    private BroadcastReceiver onScanResultsReceiver = new BroadcastReceiver() {

        @Override
        public void onReceive(Context context, Intent intent) {
            List<ScanResult> list = wiFiNetworkTracker.getResults();
            double longitude = LocationTracker.getInstance(getApplicationContext()).getLongitude();
            double latitude = LocationTracker.getInstance(getApplicationContext()).getLatitude();
            String date = new SimpleDateFormat("dd-MM-yyyy HH:mm:ss").format(Calendar.getInstance().getTime());
            if (list == null || longitude == 0.0 || latitude == 0.0) {
                return;
            }
            JSONArray resultsJsonArray = new JSONArray();
            try {
                for (ScanResult result : list) {
                    JSONObject json = new JSONObject()
                            .accumulate("SSID", result.SSID)
                            .accumulate("BSSID", result.BSSID)
                            .accumulate("auth_type", result.capabilities)
                            .accumulate("frequency", result.frequency)
                            .accumulate("date", date)
                            .accumulate("RSSI", result.level);
                    resultsJsonArray = resultsJsonArray.put(json);
                }

                String json = new JSONObject()
                        .accumulate("device_id", DEVICE_ID)
                        .accumulate("location", new JSONObject()
                                .accumulate("longitude", longitude)
                                .accumulate("latitude", latitude))
                        .accumulate("results", resultsJsonArray).toString();

                DataSender.post(
                        getApplicationContext(),
                        "tracking",
                        new JSONObject()
                                .accumulate("device_id", DEVICE_ID)
                                .accumulate("location", new JSONObject()
                                        .accumulate("longitude", longitude)
                                        .accumulate("latitude", latitude))
                                .accumulate("results", resultsJsonArray).toString(),
                        new AsyncHttpResponseHandler() {
                            @Override
                            public void onSuccess(int statusCode, Header[] headers, byte[] responseBody) {
                                Log.i(getString(R.string.AppIdentificator), "Success post");
                            }

                            @Override
                            public void onFailure(int statusCode, Header[] headers, byte[] responseBody, Throwable error) {
                                Log.e(getString(R.string.AppIdentificator), "Fail post. Status code: " + statusCode);
                            }
                        });
            } catch (JSONException | UnsupportedEncodingException e) {
                Log.e(getString(R.string.AppIdentificator), "Error: " + e.getMessage());
                e.printStackTrace();
            }
        }
    };
}