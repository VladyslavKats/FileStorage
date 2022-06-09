using AutoMapper;
using FileStorage.BLL.Interfaces;
using FileStorage.BLL.Models;
using FileStorage.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.BLL
{
    public class DocumentService : IDocumentService
    {
        private readonly IStorageUW _context;
        private readonly IMapper _mapper;

        public DocumentService(IStorageUW context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public Task<DocumentDto> AddAsync(DocumentDto document, string directory)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DocumentDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUrlAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<DocumentDto> UpdateAsync(DocumentDto document)
        {
            throw new NotImplementedException();
        }
    }
}
