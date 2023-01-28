using AutoMapper;
using FileStorage.BLL.Interfaces;
using FileStorage.PL.Models;
using FileStorage.PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileStorage.PL.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentService _documentService;
        private readonly IMapper _mapper;
        private readonly CurrentUser _currentUser;

        public DocumentsController(IDocumentService documentService, IMapper mapper, CurrentUser currentUser)
        {
            _documentService = documentService;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<DocumentViewModel>>> GetAllAsync()
        {
            var documents = await _documentService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<DocumentViewModel>>(documents));
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocumentViewModel>>> GetAllByUserAsync()
        {
            var documents = await _documentService.GetAllByUserAsync(_currentUser.GetId());
            return Ok(_mapper.Map<IEnumerable<DocumentViewModel>>(documents));
        }

        [HttpPost("upload")]
        public async Task<ActionResult> AddAsync([FromForm] IEnumerable<IFormFile> files)
        {
            await _documentService.AddAsync(files, _currentUser.GetId());
            return Ok();
        }

        [HttpPost]
        [Route("download/{id}")]
        public async Task<ActionResult> DownloadAsync(string id)
        {
            var result = await _documentService.DownloadAsync(id, _currentUser.GetId());
            return new FileContentResult(result.Document, result.ContentType)
            {
                FileDownloadName = result.Name
            };
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            await _documentService.DeleteAsync(id, _currentUser.GetId());
            return Ok();
        }
    }
}
