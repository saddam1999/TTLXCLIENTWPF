namespace TTLX.WindowsTool.Common.Http
{
    using System;
    using System.Runtime.CompilerServices;

    public class BaseException : BaseResponse
    {
        public const int ReturnedError = 0x186a0;
        public const int ServerError = 0xf1b31;
        public const int JsonError = 0xf1b32;
        public const int RestError = 0xf1b33;
        public const int OtherError = 0xf1b34;

        public BaseException()
        {
        }

        public BaseException(int code, string message)
        {
            this.code = code;
            this.message = message;
        }

        public int code { get; set; }

        public string message { get; set; }
    }
}

