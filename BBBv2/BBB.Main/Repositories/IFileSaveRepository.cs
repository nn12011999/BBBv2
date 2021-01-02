using BBB.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BBB.Main.Repositories
{
    public interface IFileSaveRepository
    {
        public IList<FileSave> GetAll();
        public IList<FileSave> GetAllWithOutData();
        public FileSave GetById(int Id);
        public FileSave GetByUrl(string url);
        public FileSave GetByIdWithOutData(int Id);
        public FileSave GetByUrlWithOutData(string url);
    }
}
