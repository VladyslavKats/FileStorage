﻿using AutoMapper;
using FileStorage.BLL.Common;
using FileStorage.BLL.Interfaces;
using FileStorage.BLL.Models;
using FileStorage.PL.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileStorage.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentService _documentService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;

        public DocumentsController(
            IDocumentService documentService  , 
            IMapper mapper , 
            IWebHostEnvironment webHostEnvironment,
            IConfiguration configuration)
        {
            _documentService = documentService;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocumentViewModel>>> GetAllAsync()
        {
            var documents = await _documentService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<DocumentViewModel>>(documents));
        }

        [HttpPost("{userName}")]
        public async Task<ActionResult<IEnumerable<DocumentViewModel>>> AddAsync(string userName,[FromForm]IEnumerable<IFormFile> files ) {
            string path = Path.Combine(_webHostEnvironment.ContentRootPath,_configuration.GetSection("Files")["DirectiveName"]);
            try
            {
                switch (files.Count())
                {
                    case 0:
                        return BadRequest();
                    case 1:
                        var document = await _documentService.AddAsync(files.First(), path, userName);
                        return Ok(_mapper.Map<IEnumerable<DocumentViewModel>>(new DocumentDto[] { document }));
                    default:
                        var documents = await _documentService.AddRangeAsync(files, path, userName);
                        return Ok(_mapper.Map<IEnumerable<DocumentViewModel>>(documents));
                }
            }
            catch (FileStorageException ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        [HttpPost]
        [Route("download")]
        public async Task<ActionResult> DownloadAsync([FromBody] DocumentDownloadModel model)
        {
            try
            {
                var bytes = await _documentService.GetDocumentBytesByPathAsync(model.Path);
                return new FileContentResult(bytes, model.ContentType) { FileDownloadName = model.FileName };
            }
            catch (FileStorageException ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}