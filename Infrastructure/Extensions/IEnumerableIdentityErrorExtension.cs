using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace Infrastructure.Extensions
{
    public static class IEnumerableIdentityErrorExtension
    {
        public static string AsJson(this IEnumerable<IdentityError> identityErrors) => JsonConvert.SerializeObject(identityErrors);
    }
}
