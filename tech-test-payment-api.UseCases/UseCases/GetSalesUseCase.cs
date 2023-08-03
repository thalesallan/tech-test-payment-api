using AutoMapper;
using FluentValidation;
using tech_test_payment.Borders.Dtos.Response;
using tech_test_payment.Borders.Repositories;
using tech_test_payment.Borders.UseCases;
using tech_test_payment.Helpers.ExceptionHelper;

namespace tech_test_payment.UseCases.UseCases
{
    public class GetSalesUseCase : IGetSalesUseCase
    {
        private readonly IValidator<int> _validator;
        private readonly ISaleRepository _paymentRepository;
        private readonly IMapper _mapper;

        public GetSalesUseCase(IValidator<int> validator, ISaleRepository paymentRepository, IMapper mapper)
        {
            _validator = validator;
            _paymentRepository = paymentRepository;
            _mapper = mapper;
        }

        public async Task<SaleResponse> Execute(int request)
        {
            try
            {
                var validationResult = _validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage);
                    var errorMessage = string.Join(", ", errors);
                    throw new ValidationException(errorMessage);
                }

                var getSalesForIdResult = await _paymentRepository.GetForSalesId(request);

                var response = _mapper.Map<SaleResponse>(getSalesForIdResult);

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
                throw;
            }
        }
    }
}
