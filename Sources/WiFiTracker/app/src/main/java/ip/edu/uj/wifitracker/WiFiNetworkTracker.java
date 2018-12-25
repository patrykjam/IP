package ip.edu.uj.wifitracker;


import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.net.wifi.ScanResult;
import android.net.wifi.WifiManager;
import android.util.Log;

import java.util.List;

import static android.content.Context.WIFI_SERVICE;

class WiFiNetworkTracker {

    private WifiManager wifiManager;
    private List<ScanResult> results;

    WiFiNetworkTracker(Context context){
        wifiManager = (WifiManager) context.getApplicationContext().getSystemService(WIFI_SERVICE);
        context.registerReceiver(WiFIBroadcast, new IntentFilter(WifiManager.SCAN_RESULTS_AVAILABLE_ACTION));
    }

    WiFiNetworkTracker(Context context, BroadcastReceiver broadcastReceiver){
        WiFIBroadcast = broadcastReceiver;
        wifiManager = (WifiManager) context.getApplicationContext().getSystemService(WIFI_SERVICE);
        context.registerReceiver(broadcastReceiver, new IntentFilter(WifiManager.SCAN_RESULTS_AVAILABLE_ACTION));
        Log.i(context.getString(R.string.AppIdentificator), "Registered service");
    }

    private BroadcastReceiver WiFIBroadcast = new BroadcastReceiver() {
        @Override
        public void onReceive(Context context, Intent intent) {
            results = wifiManager.getScanResults();
        }
    };

    public List<ScanResult> getResults() {
        return wifiManager.getScanResults();
    }

    public boolean startScan(){
        return wifiManager.startScan();
    }

    BroadcastReceiver getWiFIBroadcast() {
        return WiFIBroadcast;
    }
}

