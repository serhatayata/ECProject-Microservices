using Core.DataAccess;
using EC.Services.PaymentAPI.Entities;

namespace EC.Services.PaymentAPI.Data.Abstract.EntityFramework
{
    public interface IEfPaymentRepository : IEntityRepository<Payment>
    {

    }
}
