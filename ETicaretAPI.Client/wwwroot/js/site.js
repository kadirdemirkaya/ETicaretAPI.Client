function DisplayPersonalNotification(message, status) {
    if (status === "Success") {
        toastr.success(message, "", { timeOut: 3000 }); // Başarılı bir toastr mesajı göstermek için
    } else {
        toastr.error(message, "", { timeOut: 3000 }); // Hata bir toastr mesajı göstermek için
    }
}