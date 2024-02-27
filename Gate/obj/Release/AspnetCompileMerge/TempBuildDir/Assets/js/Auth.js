
const Consultar = () => {

    let usr = document.getElementById("email").value;
    let pwd = document.getElementById("password").value;

    if (usr == "") {
        Swal.fire({
            icon: 'error',
            title: 'INGRESA USUARIO',
            text: '',
        })
        return
    }
    else if (pwd == "") {
        Swal.fire({
            icon: 'error',
            title: 'INGRESA CONTRASEÑA',
            text: '',
        })
        return
    }

    $.ajax({

        url: '/Auth/validaLogin',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: JSON.stringify({ UsuarioPar: usr, PswPar: pwd }),
        success: function (data1) {
            console.log(data1)

            if (data1.Username != null) {
                if (data1.Enable == false) {
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
                        title: 'El usuario ya se encuentra en uso, cierre sesión o entre con otro usuario',
                        text: ""
                    })
                }
                else {
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
                        icon: 'success',
                        title: 'Usuario Correcto'
                    })
                    setTimeout(() => window.location.href = '/Auth/ValidaRol', 2000)
                }
            }
            else {
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
                    title: 'Usuario y/O Contraseña Incorrecta',
                    text: ""
                })
            }
        }
    });

}

document.addEventListener("keydown", function (event) {
    // Verifica si la tecla presionada es 'Enter' (código 13)
    if (event.key === "Enter" || event.keyCode === 13) {

        let usr = document.getElementById("email").value;
        let pwd = document.getElementById("password").value;

        if (usr == "") {

            Swal.fire({
                icon: 'error',
                title: 'INGRESA USUARIO',
                text: '',
                timer: 1000, // 3000 milisegundos = 3 segundos
                showConfirmButton: false // Esto oculta automáticamente el botón "Aceptar" (puedes quitarlo si lo deseas)
            });

            return
        }
        else if (pwd == "") {

            Swal.fire({
                icon: 'error',
                title: 'INGRESA CONTRASEÑA',
                text: '',
                timer: 1000, // 3000 milisegundos = 3 segundos
                showConfirmButton: false // Esto oculta automáticamente el botón "Aceptar" (puedes quitarlo si lo deseas)
            });

            return
        }

        $.ajax({

            url: '/Auth/validaLogin',//url: '@Url.Action("ValidaLogin", "Auth")',
            data: JSON.stringify({ UsuarioPar: usr, PswPar: pwd }),
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: function (data1) {
                console.log(data1)

                if (data1.Username != null) {
                    if (data1.Enable == false) {
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
                            title: 'El usuario ya se encuentra en uso, cierre sesión o entre con otro usuario',
                            text: ""
                        })
                    }
                    else {
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
                            icon: 'success',
                            title: 'Usuario Correcto'
                        })
                        setTimeout(() => window.location.href = '/Auth/ValidaRol', 2000)
                    }
                }
                else {
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
                        title: 'Usuario y/O Contraseña Incorrecta',
                        text: ""
                    })
                }
            }
        });

    }
});