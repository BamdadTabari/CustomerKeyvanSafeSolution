namespace KeyvanSafe.Shared.Certain.Configs;

// for saving file operation
public class MinioConfig
{
    public string Connection { get; set; } = "";
    public string AccessKey { get; set; } = "";
    public string SecretKey { get; set; } = "";
    public string RootBucketName { get; set; } = "";
}
