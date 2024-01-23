﻿namespace TaktTusur.Media.Clients.VkApi.Models
{
    public class VkApiException: Exception
    {
        public int ErrorCode {get; set;}
        
        public VkApiException(int errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}