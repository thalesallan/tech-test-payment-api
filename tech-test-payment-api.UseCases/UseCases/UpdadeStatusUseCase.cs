using FluentValidation;
using tech_test_payment.Borders.Entities;
using tech_test_payment.Borders.Repositories;
using tech_test_payment.Borders.UseCases;
using tech_test_payment.Helpers;
using tech_test_payment.Helpers.Constants;
using tech_test_payment.Helpers.Enums;
using tech_test_payment.Helpers.ExceptionHelper;

namespace tech_test_payment.UseCases.UseCases
{
    public class UpdadeStatusUseCase : IUpdateStatusUseCase
    {
        private readonly IValidator<StatusEnum> _validator;
        private readonly ISaleRepository _salesRepository;

        public UpdadeStatusUseCase(IValidator<StatusEnum> validator, ISaleRepository salesRepository)
        {
            _validator = validator;
            _salesRepository = salesRepository;
        }

        public async Task<bool> Execute(int saleId, StatusEnum status)
        {
            try
            {
                var validationResult = _validator.Validate(status);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage);
                    var errorMessage = string.Join(", ", errors);
                    throw new ValidationException(errorMessage);
                }

                SaleEntitie beforeSales = await _salesRepository.GetForSalesId(saleId);
                List<ProductEntitie> productEntities = new();

                beforeSales.Products!.ToList().ForEach(x =>
                {
                    productEntities.Add(new ProductEntitie()
                    {
                        ProductId = x.ProductId,
                        Name = x.Name,
                        Description = x.Description,
                        Price = x.Price,
                    });
                });

                SaleEntitie saleEntitie = new()
                {
                    SaleId = beforeSales.SaleId,
                    Order = beforeSales.Order,
                    Status = ValidateStatus(beforeSales, status),
                    Date = beforeSales.Date,
                    Products = productEntities,
                    Salesperson = new SalespersonEntitie
                    {
                        PersonId = beforeSales.Salesperson!.PersonId,
                        Name = beforeSales.Salesperson!.Name,
                        DocumentNumber = beforeSales.Salesperson!.DocumentNumber,
                        EmailAddress = beforeSales.Salesperson!.EmailAddress,
                        PhoneNumber = beforeSales.Salesperson!.PhoneNumber
                    }
                };

                var response = _salesRepository.UpdateStatus(saleId, saleEntitie);

                return response.Result;
            }
            catch (CustonException ex)
            {
                Console.WriteLine(ex.Message);
                throw new CustonException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                throw new Exception(ex.InnerException.Message);
            }
        }


        private static StatusEnum ValidateStatus(SaleEntitie saleEntitie, StatusEnum statusEnum)
        {
            if (saleEntitie.Status.Equals(StatusEnum.AguardandoPagamento))
            {
                if (statusEnum == StatusEnum.PagamentoAprovado)
                {
                    return statusEnum;
                }
                else if (statusEnum == StatusEnum.Cancelada)
                {
                    return statusEnum;
                }
            }
            else if (saleEntitie.Status.Equals(StatusEnum.PagamentoAprovado))
            {
                if (statusEnum == StatusEnum.EnviadoParaTransportadora)
                {
                    return statusEnum;
                }
                else if (statusEnum == StatusEnum.Cancelada)
                {
                    return statusEnum;
                }
            }
            else if (saleEntitie.Status.Equals(StatusEnum.EnviadoParaTransportadora))
            {
                if (statusEnum == StatusEnum.Entregue)
                {
                    return statusEnum;
                }
            }
            else if (saleEntitie.Status.Equals(StatusEnum.Cancelada))
            {
                throw new CustonException(
                    string.Format(ErrorsConstants.STATUS_CANCELED_VALIDATION_ERROR,
                    GetStatusStringHelper.GetStringStatus(statusEnum),
                    GetStatusStringHelper.GetStringStatus(saleEntitie.Status))
                );
            }

            throw new CustonException(
                    string.Format(ErrorsConstants.STATUS_VALIDATION_ERROR,
                    GetStatusStringHelper.GetStringStatus(statusEnum),
                    GetStatusStringHelper.GetStringStatus(saleEntitie.Status))
                );
        }
    }
}
