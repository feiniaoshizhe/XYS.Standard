using Microsoft.Extensions.DependencyInjection;

namespace XYS.Standard.Mail.AliCloud
{
    public static class AliCloudServicesExtension
    {
        public static IServiceCollection ConfigClinkSdk(this IServiceCollection service, string AccessKeyId, string AccessKeySecret)
        {
            service.AddSingleton(new AliCloudConfig()
            {
                AccessKeyId = AccessKeyId,
                AccessKeySecret = AccessKeySecret,
            });
            service.AddHttpClient<AliCloudClient>();
            return service;
        }
    }
}
