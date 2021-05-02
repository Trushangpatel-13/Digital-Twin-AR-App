using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.Android;

public class WeatherManager : MonoBehaviour
{


    [Header("UI")]
    public TextMeshProUGUI id;
    public TextMeshProUGUI voltage;
    public TextMeshProUGUI current;
    public TextMeshProUGUI power;
    public TextMeshProUGUI overload;
    public TextMeshProUGUI status;
    public TextMeshProUGUI units;
    //public TextMeshProUGUI temp_min;
    //public TextMeshProUGUI temp_max;
    //public TextMeshProUGUI pressure;
    //public TextMeshProUGUI humidity;
    //public TextMeshProUGUI windspeed;
    //private LocationInfo lastLocation;

  //  public TextMeshProUGUI text;
    void Start() {
        //StartCoroutine(FetchWeatherDataFromApi());
    }
    void Update()
    {
        StartCoroutine(FetchWeatherDataFromApi());
    }

    private IEnumerator FetchWeatherDataFromApi()
    {
        int maxWait = 3;
        while (maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }
        //string url = "" + latitude + "&lon=" + longitude + "&appid=a4090eff80166f366f980227d8292fcf&units=metric";
        string url = "https://digital-twin-api-server.herokuapp.com/api/v1?windmill=WM01";
        //statusText.text = "Inside API pulling";
        UnityWebRequest fetchWeatherRequest = UnityWebRequest.Get(url);
        yield return fetchWeatherRequest.SendWebRequest();
            if (fetchWeatherRequest.isNetworkError || fetchWeatherRequest.isHttpError)
        {
            //Check and print error 
            //statusText.text = fetchWeatherRequest.error;
        }
        else
        {
            Debug.Log(fetchWeatherRequest.downloadHandler.text);
            var response = JSON.Parse(fetchWeatherRequest.downloadHandler.text);
            id.text = "ID: " + response["ID"];
            //country.text = response["sys"]["country"];
            //statusText.text = response["name"];
            voltage.text = "Volt: " + response["Voltage"] + " V";
            current.text = "Amp: "+response["Current"] + " C";
            power.text = "Power: " + response["Power"] + " kW";
            overload.text = "Overload: " + response["Overload"];
            status.text = "Status: " + response["Status"];
            units.text = "Units: " + response["Units"];

            //feels_like.text = "Feels like " + response["main"]["feels_like"] + " C";
            //temp_min.text = "Min is " + response["main"]["temp_min"] + " C";
            //temp_max.text = "Max is " + response["main"]["temp_max"] + " C";
            //pressure.text = "Pressure is " + response["main"]["pressure"] + " Pa";
            //humidity.text = "Hum: " + response["main"]["humidity"] + " %";
            //windspeed.text = "Windspeed is " + response["wind"]["speed"] + " Km/h";
             maxWait = 3;
            //while (maxWait > 0)
            //{
                yield return new WaitForSeconds(5);
            //    maxWait--;
            //}
        }
    }
}