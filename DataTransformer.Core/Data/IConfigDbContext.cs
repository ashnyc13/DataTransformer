using DataTransformer.Core.Config;
using System.Threading.Tasks;

namespace DataTransformer.Core.Data
{
    public interface IConfigDbContext
    {
        Task Save(LibraryConfiguration libraryConfig);
    }
}