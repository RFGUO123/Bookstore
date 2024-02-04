function alertTest(msg) {
        Swal.fire({
            title: msg,
            allowOutsideClick: false
        });
}

alertOriginal = alert;
alert = alertTest;
function confirmTest(msg) {
    Swal.fire({
        title: msg,
        allowOutsideClick: false
    });
}

function confirm_for_AddCar(msg, PId) {
        Swal.fire({
            title: msg,
            allowOutsideClick: false
        }).then(function (result) {
            if (result.value) {
                
            }
            else {
                
            }
        });;
}
