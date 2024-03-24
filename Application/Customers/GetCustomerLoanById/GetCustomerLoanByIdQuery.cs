using Application.Interfaces;

namespace Application.Customers.GetCustomerLoanById;
public sealed record GetCustomerLoanByIdQuery(Guid customerId) : IQuery<CustomerLoanResponse>;
