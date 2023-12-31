﻿using TravelWarrants.DTOs;
using TravelWarrants.DTOs.Payments;

namespace TravelWarrants.Interfaces
{
    public interface IPaymentsService
    {
        Task<ResponseDTO<IEnumerable<PaymentsDTO>>> GetPayments();
        Task<ResponseDTO<PaymentsDTO>> GetPayment(int id);
        Task<ResponseDTO<PaymentsDTO>> NewPayment(PaymentsDTOSave paymentsDTO);
        Task<ResponseDTO<PaymentsDTO>> EditPayment(int id, PaymentsDTOSave paymentsDTO);
    }
}
