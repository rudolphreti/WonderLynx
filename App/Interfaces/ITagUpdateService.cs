public interface ITagUpdateService
{
    Task<HttpResponseMessage> UpdateTagsForReferenceAsync(int referenceId, List<int> tagIds);
}
