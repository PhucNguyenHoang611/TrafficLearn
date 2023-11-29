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

        public FileServices(IConfiguration configuration)
        {
            _configuration = configuration;

            FirebaseApiKey = _configuration["Storage:Firebase:ApiKey"];
            FirebaseStorageBucket = _configuration["Storage:Firebase:StorageBucket"];
            FirebaseAuthenticationEmail = _configuration["Authentication:Firebase:Email"];
            FirebaseAuthenticationPassword = _configuration["Authentication:Firebase:Password"];
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