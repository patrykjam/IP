package ip.edu.uj.wifitracker;

import android.content.Context;
import android.util.Log;

import com.loopj.android.http.AsyncHttpClient;
import com.loopj.android.http.AsyncHttpResponseHandler;
import com.loopj.android.http.RequestParams;

import java.io.UnsupportedEncodingException;

import cz.msebera.android.httpclient.entity.ContentType;
import cz.msebera.android.httpclient.entity.StringEntity;

public class DataSender {

    private static final String BASE_URL = "http://wifi-server.azurewebsites.net/";

    private DataSender(){}

    private static AsyncHttpClient client = new AsyncHttpClient();

    public static void post(Context context, String url, String json, AsyncHttpResponseHandler responseHandler) throws UnsupportedEncodingException {
        Log.i("TRACKER", "Posting to " + getAbsoluteUrl(url));
        client.post(context, getAbsoluteUrl(url), new StringEntity(json), ContentType.APPLICATION_JSON.getMimeType(), responseHandler);
    }

    private static String getAbsoluteUrl(String relativeUrl) {
        return BASE_URL + relativeUrl;
    }
}
