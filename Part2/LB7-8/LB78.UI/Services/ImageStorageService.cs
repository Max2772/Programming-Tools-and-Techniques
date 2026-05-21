namespace LB78.UI.Services;

public static class ImageStorageService
{
    public static string ImagesDirectory =>
        Path.Combine(FileSystem.AppDataDirectory, "Images");

    public static string GetImagePath(int entityId) =>
        Path.Combine(ImagesDirectory, $"{entityId}.jpg");

    public static async Task<string?> SaveImageAsync(int entityId, string? sourcePath)
    {
        if (string.IsNullOrEmpty(sourcePath) || !File.Exists(sourcePath))
            return null;

        Directory.CreateDirectory(ImagesDirectory);
        string targetFile = GetImagePath(entityId);

        await using (var sourceStream = File.OpenRead(sourcePath))
        await using (var destinationStream = File.Create(targetFile))
        {
            await sourceStream.CopyToAsync(destinationStream);
        }

        return targetFile;
    }
}
