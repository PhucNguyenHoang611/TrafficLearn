using Azure.Security.KeyVault.Secrets;
using Firebase.Auth;
using Firebase.Storage;

namespace webapi.Services
{
    public class FileServices
    {
        private readonly IConfiguration _configuration;
        private static string? FirebaseApiKey;
        private static string? FirebaseStorageBucket;
        private static string? FirebaseAuthenticationEmail;
        private static string? FirebaseAuthenticationPassword;
        private readonly SecretClient _secretClient;

        public FileServices(IConfiguration configuration, SecretClient secretClient)
        {
            _configuration = configuration;
            _secretClient = secretClient;

            /*FirebaseApiKey = _configuration["Storage:Firebase:ApiKey"];
            FirebaseStorageBucket = _configuration["Storage:Firebase:StorageBucket"];
            FirebaseAuthenticationEmail = _configuration["Authentication:Firebase:Email"];
            FirebaseAuthenticationPassword = _configuration["Authentication:Firebase:Password"];*/

            FirebaseApiKey = _secretClient.GetSecret("Storage-Firebase-ApiKey").Value.Value.ToString();
            FirebaseStorageBucket = _secretClient.GetSecret("Storage-Firebase-StorageBucket").Value.Value.ToString();
            FirebaseAuthenticationEmail = _secretClient.GetSecret("Authentication-Firebase-Email").Value.Value.ToString();
            FirebaseAuthenticationPassword = _secretClient.GetSecret("Authentication-Firebase-Password").Value.Value.ToString();
        }

        public async Task<string> UploadToFirebaseStorage(MemoryStream memoryStream, string fileName)
        {
            var authentication = new FirebaseAuthProvider(new FirebaseConfig(FirebaseApiKey));
            var a = await authentication.SignInWithEmailAndPasswordAsync(FirebaseAuthenticationEmail, FirebaseAuthenticationPassword);

            var cancellation = new CancellationTokenSource();

            var task = new FirebaseStorage(
                FirebaseStorageBucket,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                    ThrowOnCancel = true
                })
                .Child("files")
                .Child(fileName)
                .PutAsync(memoryStream, cancellation.Token);

            return await task;
        }
    }
}