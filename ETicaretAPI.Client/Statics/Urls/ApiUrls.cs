namespace ETicaretAPI.Client.Statics.Urls
{
    public static class ApiUrls
    {
        public static class Chat
        {
            public static string MessageCreateAsync = "https://localhost:7107/api/Chat/MessageCreateAsync";
        }
        public static class ProductUrls
        {
            public static string AddProduct = "https://localhost:7107/api/product/AddProduct";
            public static string GetAllProduct = "https://localhost:7107/api/product/GetAllProduct";
            public static string getProductByGuid = "https://localhost:7107/api/product/GetProductByGuid";
            public static string productUpdate = "https://localhost:7107/api/product/productUpdate";
            public static string productDelete = "https://localhost:7107/api/product/ProductDelete";
            public static string producAddPhoto = "https://localhost:7107/api/product/ProducAddPhoto";
            public static string getProductWithCode = "https://localhost:7107/api/product/GetProductWithCode";
            public static string deleteProductImageByProductId = "https://localhost:7107/api/product/DeleteProductImageByProductId";
            public static string getProductImagesByProductId = "https://localhost:7107/api/product/GetProductImageByProductId";
            public static string deleteProductImageByGuid = "https://localhost:7107/api/product/DeleteProductImageByGuid";
        }

        public static class Basket
        {
            public static string AddBasket = "https://localhost:7107/api/basket/AddBasket";
            public static string GetBasketForUser = "https://localhost:7107/api/basket/GetBasketForUser";
            public static string DeleteInBasketProduct = "https://localhost:7107/api/basket/DeleteInBasketProduct";
            public static string ConfirmBasket = "https://localhost:7107/api/basket/ConfirmBasket";
        }

        public static class Order
        {
            public static string GetNotActiveOrders = "https://localhost:7107/api/order/GetNotActiveOrders";
            public static string GetActiveOrders = "https://localhost:7107/api/order/GetActiveOrders";
            public static string CancelToOrderById = "https://localhost:7107/api/order/CancelToOrderById";
        }

        public static class Category
        {
            public static string ProductTotalOfCategories = "https://localhost:7107/api/Category/ProductTotalOfCategories";
            public static string GetAllCategory = "https://localhost:7107/api/Category/GetAllCategory";
        }

        public static class Communication
        {
            public static string GetRoleUser = "https://localhost:7107/api/authenticate/GetRoleUser";
            public static string CommunicationInfoForUser = "https://localhost:7107/api/Communication/CommunicationInfoForUser";
            public static string CommunicationInfoForPerson = "https://localhost:7107/api/Communication/CommunicationInfoForCommunicationPerson";
            public static string CommunicationEnd = "https://localhost:7107/api/Communication/CommunicationEnd";
            public static string CommunicationCreate = "https://localhost:7107/api/Communication/CommunicationCreate";
            public static string CommunicationEndForAppuserId = "https://localhost:7107/api/Communication/CommunicationEndForAppuserId";
        }

        public static class Roles
        {
            public static string User = "User";
            public static string CommunicationPerson = "CommunicationPerson";

        }

        public static string Login = "https://localhost:7107/api/authenticate/Login";
        public static string Register = "https://localhost:7107/api/authenticate/register";
        public static string TwoFactorAuthentication = "https://localhost:7107/api/authenticate/TwoFactorAuthentication";


    }
}
