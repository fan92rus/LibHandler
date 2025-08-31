using LibHandler;
using LibHandler.Models;

Mirror downloadMirror = new Mirror()
{
    Url = "library.gs",
    FullUrl = "https://libgen.gs",
    MirrorType = MirrorType.DownloadMirror,
};

LibraryHandler.SetCurrentMirror(downloadMirror);
string request = LibraryHandler.GetRequestFormat(SearchField.Title, "10.1093/jac/dkw220", 25);

List<string> ids = LibraryHandler.GetIDs(request);

// var books = LibraryHandler.GetBooks(request);

var links = LibraryHandler.GetDownloadLinks(ids.First());
;
;