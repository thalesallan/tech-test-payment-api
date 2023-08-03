namespace tech_test_payment.Helpers.Constants
{
    public class ErrorsConstants
    {
        // Errors Validator
        public const string VALIDATION_NAME = "Name must be informed.";
        public const string VALIDATION_IDENTIFICATION_NUMBER = "Identification number must be informed.";
        public const string VALIDATION_IDENTIFICATION_NUMBER_INVALID = "Identification is invalid.";
        public const string VALIDATION_EMAIL = "Email must be informed.";
        public const string VALIDATION_EMAIL_INVALID = "Email entered is invalid.";
        public const string VALIDATION_PHONE_NUMBER = "Phone number must be informed.";
        public const string VALIDATION_PHONE_NUMBER_INVALID = "Phone number entered is invalid.";
        public const string VALIDATION_DESCRIPTION = "Description cannot be empty.";
        public const string VALIDATION_PRICE = "Price cannot be zero or negative.";
        public const string VALIDATION_ID = "Id must be informed.";
        public const string VALIDATION_STATUS = "Status entered is invalid.";
        public const string VALIDATION_PRODUCT = "There must be at least one product informed.";
        public const string VALIDATION_SALLER = "Seller was not informed.";

        //Errors Repositories
        public const string REPOSITORY_GET_SALE = "Repository Error: Unable to get sale.";
        public const string REPOSITORY_CREATE_SALE = "Repository Error: Unable to create a sale.";
        public const string REPOSITORY_UPDATE_SALE_STATUS = "Repository Error: Unable to update sales status.";

        //Errors Status Validador
        public const string STATUS_VALIDATION_ERROR = "It is not possible to update the status to '{0}' because it is still '{1}'.";
        public const string STATUS_CANCELED_VALIDATION_ERROR = "It is not possible to change the status to '{0}' because the sale is '{1}'.";


    }
}
