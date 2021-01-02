﻿using BBB.Data;
using BBB.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBB.Main.Repositories
{
    public class FileSaveRepository : IFileSaveRepository
    {
        private ApplicationDbContext _context;
        public FileSaveRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<FileSave> GetAll()
        {
            return _context.FileSaves.ToList();
        }

        public IList<FileSave> GetAllWithOutData()
        {
            return _context.FileSaves.Select(x => new FileSave {
                FileName = x.FileName,
                FileType = x.FileType ,
                Id = x.Id,
                Url = x.Url})
            .ToList();
        }

        public FileSave GetById(int Id)
        {
            return  _context.FileSaves.Find(Id);
        }

        public FileSave GetByIdWithOutData(int Id)
        {
            return _context.FileSaves.Where(x=> x.Id == Id)
                .Select(x=>new FileSave {
                        Id = x.Id,
                        FileName = x.FileName,
                        Url = x.Url})
                .FirstOrDefault();
        }

        public FileSave GetByUrl(string url)
        {
            return _context.FileSaves.Where(x=> x.Url == url).FirstOrDefault();
        }

        public FileSave GetByUrlWithOutData(string url)
        {
            return _context.FileSaves.Where(x => x.Url == url)
                .Select(x => new FileSave
                {
                    Id = x.Id,
                    FileName = x.FileName,
                    Url = x.Url
                })
                .FirstOrDefault();
        }
    }
}
