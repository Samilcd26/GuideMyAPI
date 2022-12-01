﻿namespace GuideAPI.Helpers
{
    public static class JwtExtension
    {
        public static void AddApplicationError(this HttpResponse response ,string message)
        {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Control-Allow-Arigin", "*");
            response.Headers.Add("Access-Control-Expose-Header","Application-Error");
        }
    }
}