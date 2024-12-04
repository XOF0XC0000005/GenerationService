﻿using GenerationService.Interfaces;
using GenerationService.Models;
using OfficeOpenXml;
using FileResult = GenerationService.Models.FileResult;


namespace GenerationService.Services
{
    public class FileService : IFileService
    {
        private readonly IFileRepository _fileRepository;

        public FileService(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public async Task<bool> DeleteFileAsync(Guid id)
        {
            var fileMeta = await _fileRepository.GetFileByIdAsync(id);

            if (fileMeta != null)
            {
                File.Delete(fileMeta.Path);
                await _fileRepository.DeleteFileAsync(id);
                return true;
            }

            return false;
        }

        public async Task<FileResult> GenerateExcelFileAsync(ExcelDataRequest request)
        {
            try
            {
                var filePath = Path.Combine("uploads", $"{Guid.NewGuid()}.xlsx");

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                    for (int i = 0; i < request.Columns.Count; i++)
                    {
                        worksheet.Cells[1, i + 1].Value = request.Columns[i];
                    }

                    for (int i = 0; i < request.Values.Count; i++)
                    {
                        for (int j = 0; j < request.Values[i].Count; j++)
                        {
                            worksheet.Cells[i + 2, j + 1].Value = request.Values[i][j];
                        }
                    }

                    Directory.CreateDirectory("uploads");
                    await package.SaveAsAsync(new FileInfo(filePath));
                }

                var fileMeta = new FileMeta
                {
                    Id = Guid.NewGuid(),
                    Name = Path.GetFileName(filePath),
                    Path = filePath,
                    Size = new FileInfo(filePath).Length,
                    CreatedAt = DateTime.UtcNow,
                };

                await _fileRepository.SaveFileMetaAsync(fileMeta);

                return new FileResult { IsSuccess = true, FilePath = filePath };
            }
            catch (Exception ex)
            {
                return new FileResult { IsSuccess = false, ErrorMessage = ex.Message };
            }
        }

        public async Task<List<FileMeta>> GetFileAsync()
        {
            return await _fileRepository.GetAllFileAsync();
        }
    }
}