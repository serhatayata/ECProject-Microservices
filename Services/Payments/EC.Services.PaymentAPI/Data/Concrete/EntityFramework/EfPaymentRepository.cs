using Core.DataAccess.EntityFramework;
using EC.Services.PaymentAPI.Data.Abstract.EntityFramework;
using EC.Services.PaymentAPI.Data.Contexts;
using EC.Services.PaymentAPI.Entities;

namespace EC.Services.PaymentAPI.Data.Concrete.EntityFramework
{
    public class EfPaymentRepository:EfEntityRepositoryBase<Payment, PaymentDbContext>, IEfPaymentRepository
    {


    }
}
