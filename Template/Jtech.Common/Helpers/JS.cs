using Jint;

namespace Jtech.Common.Helpers
{
    public static class JS
    {
        public static object Eval(string script, string callFunc, params object[] args)
        {
            var engine = new Engine(cfg => cfg
                                .AllowClr()
                                .AllowClr(typeof(System.IO.File).Assembly)
                         ).Execute(script);
            return engine.Invoke(callFunc, args);
        }
    }
}
