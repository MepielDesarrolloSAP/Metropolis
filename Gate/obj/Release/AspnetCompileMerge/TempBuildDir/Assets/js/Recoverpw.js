
const Recover = () => {

    let usr = document.getElementById("email").value;


    if (usr == "") {
        Swal.fire({
            icon: 'error',
            title: 'Ingresa un usuario o correo válido para continuar.',
            text: '',
        })
        return
    }

    //console.log("dsddf")
    //return

    $.ajax({

        url: '/Auth/Recover',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: JSON.stringify({ UsuarioPar: usr }),
        success: function (data1) {
            console.log(data1)

            if (data1.Username == null) {
                const Toast = Swal.mixin({
                    toast: true,
                    position: 'top-end',
                    showConfirmButton: false,
                    timer: 3000,
                    timerProgressBar: true,
                    didOpen: (toast) => {
                        toast.addEventListener('mouseenter', Swal.stopTimer)
                        toast.addEventListener('mouseleave', Swal.resumeTimer)
                    }
                })
                Toast.fire({
                    icon: 'error',
                    title: 'Ingresa un usuario o correo válido',
                    text: ""
                })
            }
            else {
                Swal.fire({
                    icon: 'success',
                    title: 'Usuario o correo valido',
                    text: 'Contraseña: ' + data1.Password,
                })
                //setTimeout(() => window.location.href = '/Auth/ValidaRol', 2000)
            }

        }
    });

}