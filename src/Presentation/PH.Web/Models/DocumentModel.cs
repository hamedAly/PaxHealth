﻿namespace PH.Web.Models;

public class DocumentModel
{
    public int Id { get; set; }
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public string FileType { get; set; }
    public string Description { get; set; }
    public DateTime DateAdded { get; set; }
    public IFormFile File { get; set; }
}