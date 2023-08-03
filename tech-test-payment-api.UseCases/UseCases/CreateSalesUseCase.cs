using AutoMapper;
using FluentValidation;
using tech_test_payment.Borders.Dtos.Request;
using tech_test_payment.Borders.Dtos.Response;
using tech_test_payment.Borders.Entities;
using tech_test_payment.Borders.Repositories;
using tech_test_payment.Borders.UseCases;
using tech_test_payment.Helpers.Enums;
using tech_test_payment.Helpers.ExceptionHelper;

namespace tech_test_payment.UseCases.UseCases
{
    public class CreateSalesUseCase : ICreateSalesUseCase
    {
        public static readonly DateTime CurrentDateTime = DateTime.Now;

        private readonly IValidator<SaleRequest> _validator;
        private readonly ISaleRepository _salesRepository;
        private readonly IMapper _mapper;

        public CreateSalesUseCase(IValidator<SaleRequest> validator, ISaleRepository paymentRepository, IMapper mapper)
        {
            _validator = validator;
            _salesRepository = paymentRepository;
            _mapper = mapper;
        }

        public async Task<SaleResponse> Execute(SaleRequest request)
        {
            try
            {
                var validationResult = _validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage);
                    var errorMessage = string.Join("\n", errors);
                    throw new ValidationException(errorMessage);
                }

                Random random = new Random();

                List<ProductEntitie> productEntities = new List<ProductEntitie>();

                request.Products!.ToList().ForEach(x =>
                {
                    productEntities.Add(new ProductEntitie()
                    {
                        ProductId = random.Next(100, 200),
                        Name = x.Name,
                        Description = x.Description,
                        Price = x.Price,
                    });
                });

                SaleEntitie sellEntitie = new SaleEntitie()
                {
                    SaleId = random.Next(100, 200),
                    Order = random.Next(1000, 2000),
                    Status = StatusEnum.AguardandoPagamento,
                    Date = CurrentDateTime,
                    Products = productEntities,
                    Salesperson = new SalespersonEntitie
                    {
                        PersonId = random.Next(100, 200),
                        Name = request.Salesperson.Name,
                        DocumentNumber = request.Salesperson.DocumentNumber,
                        EmailAddress = request.Salesperson.EmailAddress,
                        PhoneNumber = request.Salesperson.PhoneNumber
                    }
                };

                var result = await _salesRepository.CreateSales(sellEntitie);

                var response = _mapper.Map<SaleResponse>(result);

                return response;
            }
            catch (CustonException ex)
            {
                Console.WriteLine(ex.Message);
                throw new CustonException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }

    }
}
