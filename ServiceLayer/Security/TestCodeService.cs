using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using Camps.DataLayer.Context;
using Comps.DomainLayer.Security;

namespace Comps.ServiceLayer.Security
{
    public class TestCodeService : ITestCodeManager
    {
        private readonly IDbSet<TestPhoneCode> _dbSet;
        private IUnitOfWork _unitOfWork;
        public TestCodeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _dbSet = unitOfWork.Set<TestPhoneCode>();
        }
        public void Add(TestPhoneCode code)
        {
            _dbSet.AddOrUpdate(code);
          //  _unitOfWork.SaveAllChanges();
        }

        public void Remove(int id)
        {
            var testPhoneCode = _dbSet.Find(id);
            if (testPhoneCode!=null)
            {
                _dbSet.Remove(testPhoneCode);
              //  _unitOfWork.SaveAllChanges();
            }
         
        }

        public void RemoveAll()
        {
            foreach (var item in  _dbSet.ToList())
            {
                _dbSet.Remove(item);
            //    _unitOfWork.SaveAllChanges();
            }
     
        }

        public TestPhoneCode Find(int id)
        {
            return    _dbSet.Find(id);

        }

        public void Dispose()
        {
            //throw new System.NotImplementedException();
        }


        public async Task<IList<TestPhoneCode>> Get()
        {
            return await _dbSet.OrderByDescending(x=>x.Id).Take(200).ToListAsync();
        }
    }
}