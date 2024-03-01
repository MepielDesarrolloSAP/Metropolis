var StartMenu = document.getElementById('StartMenu');

var UsersMenu = document.getElementById('UsersMenu');

var AddressMenu = document.getElementById('AddressMenu');

var SRMenu = document.getElementById('SRMenu');

var route = "sin ruta";

const LogOut = () => {
    swal.fire({
        title: "Cerrar Sesion",
        text: "¿Desas Salir?",
        icon: "info",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        cancelButtonText: 'No',
        confirmButtonText: 'Si'
    })
        .then((LogOut) => {
            if (LogOut.isConfirmed) {
                location.href = '/Auth/LogOut';
            }
        });
}

const Users = () => {

    StartMenu.style.color = "#6c757d";
    UsersMenu.style.color = "#3a57e8";
    AddressMenu.style.color = "#6c757d";
    SRMenu.style.color = "#6c757d";


    //console.log("Usuarios");
    document.getElementById('CuerpoGeneral').style.display = 'Flex';
    document.getElementById('btonUseradd').style.display = 'Flex';

    document.getElementById('ContainerAddress').style.display = 'none';
    document.getElementById('btonAddressadd').style.display = 'none';

    document.getElementById('ContaiRoute').style.display = 'none';
    document.getElementById('buttonsRoute').style.display = 'none';

    document.getElementById('GeneratedRoutes').style.display = 'none';

    document.getElementById('MsjOne').innerHTML = 'Definicion de usuarios';
    document.getElementById('MsjTwo').innerHTML = '¡Bienvenido, Administrador! Gestiona usuarios y mejora el sistema con privilegios especiales. Gracias por tu servicio';


    $.ajax({

        url: '/Home/Users',
        //data: JSON.stringify({ UsuarioPar: usr, PswPar: pwd }),
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data1) {

            //console.log(data1)
            $('#Data-Usuarios').bootstrapTable({ data: data1 })

        }

    });

}

const Address = () => {

    StartMenu.style.color = "#6c757d";
    UsersMenu.style.color = "#6c757d";
    AddressMenu.style.color = "#3a57e8";
    SRMenu.style.color = "#6c757d";


    document.getElementById('CuerpoGeneral').style.display = 'none';
    document.getElementById('btonUseradd').style.display = 'none';

    document.getElementById('ContainerAddress').style.display = 'Flex';
    document.getElementById('btonAddressadd').style.display = 'Flex';

    document.getElementById('ContaiRoute').style.display = 'none';
    document.getElementById('buttonsRoute').style.display = 'none';

    document.getElementById('GeneratedRoutes').style.display = 'none';

    document.getElementById('MsjOne').innerHTML = 'Definicion de direcciones';
    document.getElementById('MsjTwo').innerHTML = '¡Bienvenido, Administrador! Gestiona direcciones y mejora el sistema con privilegios especiales. Gracias por tu servicio';


    $.ajax({

        url: '/Home/Address',
        //data: JSON.stringify({ UsuarioPar: usr, PswPar: pwd }),
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data1) {

            //console.log(data1)
            $('#Data-Adress').bootstrapTable({ data: data1 })

        }

    });

}

const RouteMCDMX = () => {

    route = "R3";

    StartMenu.style.color = "#6c757d";
    UsersMenu.style.color = "#6c757d";
    AddressMenu.style.color = "#6c757d";
    SRMenu.style.color = "#3a57e8";

    document.getElementById('CuerpoGeneral').style.display = 'none';
    document.getElementById('btonUseradd').style.display = 'none';

    document.getElementById('ContainerAddress').style.display = 'none';
    document.getElementById('btonAddressadd').style.display = 'none';

    document.getElementById('ContaiRoute').style.display = 'Flex';
    document.getElementById('buttonsRoute').style.display = 'Flex';

    document.getElementById('GeneratedRoutes').style.display = 'none';

    document.getElementById('MsjOne').innerHTML = 'Definicion de rutas (Ruta de la mañana CDMX)';
    document.getElementById('MsjTwo').innerHTML = '¡¡Bienvenido, Planificador! Diseña rutas eficientes y asegura entregas puntuales con tus habilidades especiales. ¡Gracias por tu compromiso con la logística y la satisfacción del cliente!';

    //console.log("Llegue aqui")
    //return

    $.ajax({

        url: '/Home/RouteMCDMX',
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data1) {

            if ($('#Data-Route').data('bootstrap.table')) {
                // La tabla está inicializada, puedes realizar operaciones en ella
                console.log("La tabla está inicializada");
                $('#Data-Route').bootstrapTable('removeAll');
                $('#Data-Route').bootstrapTable('refreshOptions', { data: data1 })
                // Aquí puedes agregar cualquier código que quieras ejecutar si la tabla está inicializada
            } else {
                // La tabla no está inicializada, debes inicializarla antes de realizar operaciones en ella
                console.log("La tabla no está inicializada");
                $('#Data-Route').bootstrapTable({ data: data1 });
                // Aquí puedes agregar el código para inicializar la tabla si es necesario
            }

        }

    });

}

const RouteTCDMX = () => {

    route = "R4";

    StartMenu.style.color = "#6c757d";
    UsersMenu.style.color = "#6c757d";
    AddressMenu.style.color = "#6c757d";
    SRMenu.style.color = "#3a57e8";

    document.getElementById('CuerpoGeneral').style.display = 'none';
    document.getElementById('btonUseradd').style.display = 'none';

    document.getElementById('ContainerAddress').style.display = 'none';
    document.getElementById('btonAddressadd').style.display = 'none';

    document.getElementById('ContaiRoute').style.display = 'Flex';
    document.getElementById('buttonsRoute').style.display = 'Flex';

    document.getElementById('GeneratedRoutes').style.display = 'none';

    document.getElementById('MsjOne').innerHTML = 'Definicion de rutas (Ruta de la tarde CDMX)';
    document.getElementById('MsjTwo').innerHTML = '¡¡Bienvenido, Planificador! Diseña rutas eficientes y asegura entregas puntuales con tus habilidades especiales. ¡Gracias por tu compromiso con la logística y la satisfacción del cliente!';

    //console.log("Llegue aqui")
    //return

    $.ajax({

        url: '/Home/RouteTCDMX',
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data1) {

            if ($('#Data-Route').data('bootstrap.table')) {
                // La tabla está inicializada, puedes realizar operaciones en ella
                console.log("La tabla está inicializada");
                $('#Data-Route').bootstrapTable('removeAll');
                $('#Data-Route').bootstrapTable('refreshOptions', { data: data1 })
                // Aquí puedes agregar cualquier código que quieras ejecutar si la tabla está inicializada
            } else {
                // La tabla no está inicializada, debes inicializarla antes de realizar operaciones en ella
                console.log("La tabla no está inicializada");
                $('#Data-Route').bootstrapTable({ data: data1 });
                // Aquí puedes agregar el código para inicializar la tabla si es necesario
            }

        }

    });

}

const RouteMGDL = () => {

    route = "R1";

    StartMenu.style.color = "#6c757d";
    UsersMenu.style.color = "#6c757d";
    AddressMenu.style.color = "#6c757d";
    SRMenu.style.color = "#3a57e8";

    document.getElementById('CuerpoGeneral').style.display = 'none';
    document.getElementById('btonUseradd').style.display = 'none';

    document.getElementById('ContainerAddress').style.display = 'none';
    document.getElementById('btonAddressadd').style.display = 'none';

    document.getElementById('ContaiRoute').style.display = 'Flex';
    document.getElementById('buttonsRoute').style.display = 'Flex';

    document.getElementById('GeneratedRoutes').style.display = 'none';

    document.getElementById('MsjOne').innerHTML = 'Definicion de rutas (Ruta de la mañana Guadalajara)';
    document.getElementById('MsjTwo').innerHTML = '¡¡Bienvenido, Planificador! Diseña rutas eficientes y asegura entregas puntuales con tus habilidades especiales. ¡Gracias por tu compromiso con la logística y la satisfacción del cliente!';

    //console.log("Llegue aqui")
    //return

    $.ajax({

        url: '/Home/RouteMGDL',
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data1) {

            if ($('#Data-Route').data('bootstrap.table')) {
                // La tabla está inicializada, puedes realizar operaciones en ella
                console.log("La tabla está inicializada");
                $('#Data-Route').bootstrapTable('removeAll');
                $('#Data-Route').bootstrapTable('refreshOptions', { data: data1 })
                // Aquí puedes agregar cualquier código que quieras ejecutar si la tabla está inicializada
            } else {
                // La tabla no está inicializada, debes inicializarla antes de realizar operaciones en ella
                console.log("La tabla no está inicializada");
                $('#Data-Route').bootstrapTable({ data: data1 });
                // Aquí puedes agregar el código para inicializar la tabla si es necesario
            }

        }

    });

}

const RouteTGDL = () => {

    route = "R2";

    StartMenu.style.color = "#6c757d";
    UsersMenu.style.color = "#6c757d";
    AddressMenu.style.color = "#6c757d";
    SRMenu.style.color = "#3a57e8";

    document.getElementById('CuerpoGeneral').style.display = 'none';
    document.getElementById('btonUseradd').style.display = 'none';

    document.getElementById('ContainerAddress').style.display = 'none';
    document.getElementById('btonAddressadd').style.display = 'none';

    document.getElementById('ContaiRoute').style.display = 'Flex';
    document.getElementById('buttonsRoute').style.display = 'Flex';

    document.getElementById('GeneratedRoutes').style.display = 'none';

    document.getElementById('MsjOne').innerHTML = 'Definicion de rutas (Ruta de la tarde Guadalajara)';
    document.getElementById('MsjTwo').innerHTML = '¡¡Bienvenido, Planificador! Diseña rutas eficientes y asegura entregas puntuales con tus habilidades especiales. ¡Gracias por tu compromiso con la logística y la satisfacción del cliente!';

    //console.log("Llegue aqui")
    //return

    $.ajax({

        url: '/Home/RouteTGDL',
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data1) {

            if ($('#Data-Route').data('bootstrap.table')) {
                // La tabla está inicializada, puedes realizar operaciones en ella
                console.log("La tabla está inicializada");
                $('#Data-Route').bootstrapTable('removeAll');
                $('#Data-Route').bootstrapTable('refreshOptions', { data: data1 })
                // Aquí puedes agregar cualquier código que quieras ejecutar si la tabla está inicializada
            } else {
                // La tabla no está inicializada, debes inicializarla antes de realizar operaciones en ella
                console.log("La tabla no está inicializada");
                $('#Data-Route').bootstrapTable({ data: data1 });
                // Aquí puedes agregar el código para inicializar la tabla si es necesario
            }

        }

    });

}

const generatedRouteMCDMX = () => {

    StartMenu.style.color = "#6c757d";
    UsersMenu.style.color = "#6c757d";
    AddressMenu.style.color = "#6c757d";
    SRMenu.style.color = "#3a57e8";

    document.getElementById('CuerpoGeneral').style.display = 'none';
    document.getElementById('btonUseradd').style.display = 'none';

    document.getElementById('ContainerAddress').style.display = 'none';
    document.getElementById('btonAddressadd').style.display = 'none';

    document.getElementById('ContaiRoute').style.display = 'none';
    document.getElementById('buttonsRoute').style.display = 'none';

    document.getElementById('GeneratedRoutes').style.display = 'Flex';

    document.getElementById('MsjOne').innerHTML = 'Definicion de rutas (Ruta de la mañana CDMX)';
    document.getElementById('MsjTwo').innerHTML = '¡¡Bienvenido, Planificador! Diseña rutas eficientes y asegura entregas puntuales con tus habilidades especiales. ¡Gracias por tu compromiso con la logística y la satisfacción del cliente!';

    //console.log("Llegue aqui")
    //return

    $.ajax({

        url: '/Home/GeneratedRoutesMCDMX',
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data1) {

            //// Verificar si la tabla tiene registros antes de intentar eliminarlos
            //if ($('#Data-Routes').bootstrapTable('getData').length > 0) {
            //    // Si la tabla tiene registros, entonces eliminar todos los registros
            //    $('#Data-Routes').bootstrapTable('destroy'); // Destruir la tabla existente
            //}

            //// Inicializar la tabla nuevamente con nuevos datos
            //$('#Data-Routes').bootstrapTable({ data: data1 });

            if ($('#Data-Routes').data('bootstrap.table')) {
                // La tabla está inicializada, puedes realizar operaciones en ella
                console.log("La tabla está inicializada");
                $('#Data-Routes').bootstrapTable('removeAll');
                $('#Data-Routes').bootstrapTable('refreshOptions', { data: data1 })
                // Aquí puedes agregar cualquier código que quieras ejecutar si la tabla está inicializada
            } else {
                // La tabla no está inicializada, debes inicializarla antes de realizar operaciones en ella
                console.log("La tabla no está inicializada");
                $('#Data-Routes').bootstrapTable({ data: data1 });
                // Aquí puedes agregar el código para inicializar la tabla si es necesario
            }


        }

    });

}

const generatedRouteTCDMX = () => {

    StartMenu.style.color = "#6c757d";
    UsersMenu.style.color = "#6c757d";
    AddressMenu.style.color = "#6c757d";
    SRMenu.style.color = "#3a57e8";

    document.getElementById('CuerpoGeneral').style.display = 'none';
    document.getElementById('btonUseradd').style.display = 'none';

    document.getElementById('ContainerAddress').style.display = 'none';
    document.getElementById('btonAddressadd').style.display = 'none';

    document.getElementById('ContaiRoute').style.display = 'none';
    document.getElementById('buttonsRoute').style.display = 'none';

    document.getElementById('GeneratedRoutes').style.display = 'Flex';

    document.getElementById('MsjOne').innerHTML = 'Definicion de rutas (Ruta de la tarde CDMX)';
    document.getElementById('MsjTwo').innerHTML = '¡¡Bienvenido, Planificador! Diseña rutas eficientes y asegura entregas puntuales con tus habilidades especiales. ¡Gracias por tu compromiso con la logística y la satisfacción del cliente!';

    //console.log("Llegue aqui")
    //return

    $.ajax({

        url: '/Home/GeneratedRoutesTCDMX',
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data1) {

            //// Verificar si la tabla tiene registros antes de intentar eliminarlos
            //if ($('#Data-Routes').bootstrapTable('getData').length > 0) {
            //    // Si la tabla tiene registros, entonces eliminar todos los registros
            //    $('#Data-Routes').bootstrapTable('destroy'); // Destruir la tabla existente
            //}

            //// Inicializar la tabla nuevamente con nuevos datos
            //$('#Data-Routes').bootstrapTable({ data: data1 });

            if ($('#Data-Routes').data('bootstrap.table')) {
                // La tabla está inicializada, puedes realizar operaciones en ella
                console.log("La tabla está inicializada");
                $('#Data-Routes').bootstrapTable('removeAll');
                $('#Data-Routes').bootstrapTable('refreshOptions', { data: data1 })
                // Aquí puedes agregar cualquier código que quieras ejecutar si la tabla está inicializada
            } else {
                // La tabla no está inicializada, debes inicializarla antes de realizar operaciones en ella
                console.log("La tabla no está inicializada");
                $('#Data-Routes').bootstrapTable({ data: data1 });
                // Aquí puedes agregar el código para inicializar la tabla si es necesario
            }


        }

    });

}

const generatedRouteMGDL = () => {

    StartMenu.style.color = "#6c757d";
    UsersMenu.style.color = "#6c757d";
    AddressMenu.style.color = "#6c757d";
    SRMenu.style.color = "#3a57e8";

    document.getElementById('CuerpoGeneral').style.display = 'none';
    document.getElementById('btonUseradd').style.display = 'none';

    document.getElementById('ContainerAddress').style.display = 'none';
    document.getElementById('btonAddressadd').style.display = 'none';

    document.getElementById('ContaiRoute').style.display = 'none';
    document.getElementById('buttonsRoute').style.display = 'none';

    document.getElementById('GeneratedRoutes').style.display = 'Flex';

    document.getElementById('MsjOne').innerHTML = 'Definicion de rutas (Ruta de la mañana Guadalajara)';
    document.getElementById('MsjTwo').innerHTML = '¡¡Bienvenido, Planificador! Diseña rutas eficientes y asegura entregas puntuales con tus habilidades especiales. ¡Gracias por tu compromiso con la logística y la satisfacción del cliente!';

    //console.log("Llegue aqui")
    //return

    $.ajax({

        url: '/Home/GeneratedRoutesMGDL',
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data1) {

            //// Verificar si la tabla tiene registros antes de intentar eliminarlos
            //if ($('#Data-Routes').bootstrapTable('getData').length > 0) {
            //    // Si la tabla tiene registros, entonces eliminar todos los registros
            //    $('#Data-Routes').bootstrapTable('destroy'); // Destruir la tabla existente
            //}

            //// Inicializar la tabla nuevamente con nuevos datos
            //$('#Data-Routes').bootstrapTable({ data: data1 });

            if ($('#Data-Routes').data('bootstrap.table')) {
                // La tabla está inicializada, puedes realizar operaciones en ella
                console.log("La tabla está inicializada");
                $('#Data-Routes').bootstrapTable('removeAll');
                $('#Data-Routes').bootstrapTable('refreshOptions', { data: data1 })
                // Aquí puedes agregar cualquier código que quieras ejecutar si la tabla está inicializada
            } else {
                // La tabla no está inicializada, debes inicializarla antes de realizar operaciones en ella
                console.log("La tabla no está inicializada");
                $('#Data-Routes').bootstrapTable({ data: data1 });
                // Aquí puedes agregar el código para inicializar la tabla si es necesario
            }


        }

    });

}

const generatedRouteTGDL = () => {

    StartMenu.style.color = "#6c757d";
    UsersMenu.style.color = "#6c757d";
    AddressMenu.style.color = "#6c757d";
    SRMenu.style.color = "#3a57e8";

    document.getElementById('CuerpoGeneral').style.display = 'none';
    document.getElementById('btonUseradd').style.display = 'none';

    document.getElementById('ContainerAddress').style.display = 'none';
    document.getElementById('btonAddressadd').style.display = 'none';

    document.getElementById('ContaiRoute').style.display = 'none';
    document.getElementById('buttonsRoute').style.display = 'none';

    document.getElementById('GeneratedRoutes').style.display = 'Flex';

    document.getElementById('MsjOne').innerHTML = 'Definicion de rutas (Ruta de la tarde Guadalajara)';
    document.getElementById('MsjTwo').innerHTML = '¡¡Bienvenido, Planificador! Diseña rutas eficientes y asegura entregas puntuales con tus habilidades especiales. ¡Gracias por tu compromiso con la logística y la satisfacción del cliente!';

    //console.log("Llegue aqui")
    //return

    $.ajax({

        url: '/Home/GeneratedRoutesTGDL',
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data1) {

            //// Verificar si la tabla tiene registros antes de intentar eliminarlos
            //if ($('#Data-Routes').bootstrapTable('getData').length > 0) {
            //    // Si la tabla tiene registros, entonces eliminar todos los registros
            //    $('#Data-Routes').bootstrapTable('destroy'); // Destruir la tabla existente
            //}

            //// Inicializar la tabla nuevamente con nuevos datos
            //$('#Data-Routes').bootstrapTable({ data: data1 });

            if ($('#Data-Routes').data('bootstrap.table')) {
                // La tabla está inicializada, puedes realizar operaciones en ella
                console.log("La tabla está inicializada");
                $('#Data-Routes').bootstrapTable('removeAll');
                $('#Data-Routes').bootstrapTable('refreshOptions', { data: data1 })
                // Aquí puedes agregar cualquier código que quieras ejecutar si la tabla está inicializada
            } else {
                // La tabla no está inicializada, debes inicializarla antes de realizar operaciones en ella
                console.log("La tabla no está inicializada");
                $('#Data-Routes').bootstrapTable({ data: data1 });
                // Aquí puedes agregar el código para inicializar la tabla si es necesario
            }

        }

    });

}

const Start = () => {

    StartMenu.style.color = "#3a57e8";
    UsersMenu.style.color = "#6c757d";
    AddressMenu.style.color = "#6c757d";
    SRMenu.style.color = "#6c757d";

    /*console.log("Inicio");*/

    document.getElementById('CuerpoGeneral').style.display = 'none';
    document.getElementById('btonUseradd').style.display = 'none';

    document.getElementById('ContainerAddress').style.display = 'none';
    document.getElementById('btonAddressadd').style.display = 'none';

    document.getElementById('ContaiRoute').style.display = 'none';
    document.getElementById('buttonsRoute').style.display = 'none';

    document.getElementById('GeneratedRoutes').style.display = 'none';

    document.getElementById('MsjOne').innerHTML = 'Hola Administrador!';
    document.getElementById('MsjTwo').innerHTML = 'Has iniciado sesión como administrador. Recuerda que cuentas con privilegios especiales para gestionar y mejorar el sistema. ¡Gracias por tu servicio! .';

}

window.operateEventsOne = {

    'click .Editar': function (e, value, row, index) {

        $.ajax({

            url: '/Home/Address',
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: function (data1) {

                // Obtener el elemento select
                var DireccionEdit = document.getElementById('ChooseAddresEdit');

                // Limpiar opciones existentes
                DireccionEdit.innerHTML = '';

                // Recorrer la lista y agregar opciones al select
                data1.forEach(function (item) {
                    var option = document.createElement('option');
                    option.Id = item.value;
                    option.text = item.Name;

                    if (item.Name == row.Direccion) {
                        console.log(item.Name)
                        option.selected = true;
                    }

                    DireccionEdit.appendChild(option);
                });
            }
        });

        $('#ModaEditUser').modal('show')

        document.getElementById("IdEdit").value = row.Id;
        document.getElementById("NameEdit").value = row.Name;
        document.getElementById("LastnameEdit").value = row.Lastname;
        document.getElementById("UsernameEdit").value = row.Username;
        document.getElementById("PasswordEdit").value = row.Password;
        document.getElementById("EmailEdit").value = row.Email;
        document.getElementById("PhoneEdit").value = row.Phone;

        if (row.Enable == true) {
            document.getElementById("EnableEdit").value = "True";
        }
        else {
            document.getElementById("EnableEdit").value = "False";

        }
        document.getElementById("Id_RoleEdit").value = row.Id_Role;
        document.getElementById("ChooseAddresEdit").value = row.Id_Address;
    
        

    },

    'click .Borrar': function (e, value, row, index) {

        
        DisableUser(row.Id);

    },

}

window.operateEventsTwo = {

    'click .Edit': function (e, value, row, index) {

        $('#ModaEditAddres').modal('show')

        document.getElementById("IDAddresEdit").value = row.Id;
        document.getElementById("NameAddressEdit").value = row.Name;
        document.getElementById("StreetEdit").value = row.Street;
        document.getElementById("ExtEdit").value = row.Ext;
        document.getElementById("CPEdit").value = row.CP;
        document.getElementById("ColonyEdit").value = row.Colony;
        document.getElementById("CityEdit").value = row.City;
        document.getElementById("CountryEdit").value = row.Country;

        if (row.Enable == true) {
            document.getElementById("EnableEditAddres").value = "True";
        }
        else {
            document.getElementById("EnableEditAddres").value = "False";

        }



    },

    'click .Disable': function (e, value, row, index) {
        DisableAddress(row.Id);

    }

}

window.operateEventsThree = {
    'click .Disable': function (e, value, row, index) {

        var primaryKey = row.Id;
        $('#Data-Route').bootstrapTable('remove', {
            field: 'Id',
            values: [primaryKey]
        });

    },
    'click .Edit': function (e, value, row, index) {

        $('#ModaEditRoute').modal('show')

        document.getElementById("IDRouteEdit").value = row.Id;
        document.getElementById("ShipToCodeRouteEdit").value = row.ShipToCode;
        document.getElementById("CardNameRouteEdit").value = row.CardName;
        document.getElementById("StreetRouteEdit").value = row.Street;
        document.getElementById("BlockRouteEdit").value = row.Block;
        document.getElementById("ZipCodeRouteEdit").value = row.ZipCode;
        document.getElementById("CityRouteEdit").value = row.City;
        document.getElementById("U_NAMERouteEdit").value = row.U_NAME;
        document.getElementById("ConditionRouteEdit").value = row.Condition;
        document.getElementById("DocNumsRouteEdit").value = row.DocNums;
        document.getElementById("CommentsRouteEdit").value = row.Comments;
        document.getElementById("PhoneRouteEdit").value = row.Phone;

    }
};

function Opciones(value, row, index) {
    console.log('valor',row.Enable == true);
    return [
        `<a class="Editar" href="javascript:void(0)"  title="Editar" >
                    `,
        `<i class='fa fa-edit' style="font-size: 20px; margin:5px; color:#236ddb;"></i>`,
        `
                </a>`,
        `<a class="Borrar" href="javascript:void(0)" ${row.Enable ? "" : "hidden"} title="Desactivar Usuario" >
                    `,
        `<i class='fa fa-remove' style="font-size: 20px; margin:5px; color:#236ddb;"></i>`,
        `
                </a>`,

    ].join('')

}

function Options(value, row, index) {
    return [
        `<a class="Edit" href="javascript:void(0)"  title="Editar direccion" >
                    `,
        `<i class='fa fa-edit' style="font-size: 20px; margin:5px; color:#236ddb;"></i>`,
        `
                </a>`,
        `<a class="Disable" href="javascript:void(0)"  title="desabilitar direccion" >
                    `,
        `<i class='fa fa-remove' style="font-size: 20px; margin:5px; color:#236ddb;"></i>`,
        `
                </a>`,

    ].join('')

}

function OptionsOne(value, row, index) {
    return [
        `<a class="Disable" href="javascript:void(0)" title="eliminar línea" data-index="${index}">
            <i class='fa fa-remove' style="font-size: 20px; margin:5px; color:#236ddb;"></i>
        </a>`,
        `<a class="Edit" href="javascript:void(0)" title="editar línea" data-index="${index}">
            <i class='fa fa-edit' style="font-size: 20px; margin:5px; color:#236ddb;"></i>
        </a>`
    ].join('');
}

function OptionsTwo(value, row, index) {

    const { Id, CreateDate, Name, Description } = row
    return [
        `<a class="like" href="javascript:void(0)" onclick="ShowRoute('${Id},${CreateDate},${Name},${Description}')" title="Like">
                    `,
        `<i class="fa fa-eye"></i>`,
        `
                </a>`,

    ].join('')

}

const ShowRoute = (row) => {

    const params = row.split(',')

    //console.log(params[0])

    $('#ModalshowRoute').modal('show')

    $.ajax({

        url: '/Home/FindMCDMX',
        data: JSON.stringify({ Id: params[0] }),
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data1) {

            // Verificar si la tabla tiene registros antes de intentar eliminarlos
            if ($('#Data-ShowRoute').bootstrapTable('getData').length > 0) {
                // Si la tabla tiene registros, entonces eliminar todos los registros
                $('#Data-ShowRoute').bootstrapTable('destroy'); // Destruir la tabla existente
            }

            $('#ModalshowRoute').modal('show')

            // Inicializar la tabla nuevamente con nuevos datos
            $('#Data-ShowRoute').bootstrapTable({ data: data1 });

        }

    });


}

function CloseModalShowRoute() {

    $('#ModalshowRoute').modal('hide')

}

function ShowModalAddUser() {

    console.log('Show modal add user');

    var selectAdressUser = 'ChooseAddres';
    //Limpiar el Select de la dirección
    CleanSelect(selectAdressUser);
    
    $.ajax({

        url: '/Home/Address',
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data1) {

            // Recorrer la lista y agregar opciones al select
            data1.forEach(function (item) {
                var option = document.createElement('option');
                option.Id = item.value;
                option.text = item.Name;

                document.getElementById('ChooseAddres').appendChild(option);

            });
        }
    });

    $('#ModalAddUser').modal('show')
    document.getElementById("Name").value = "";
    document.getElementById("Lastname").value = "";
    document.getElementById("Username").value = "";
    document.getElementById("Password").value = "";
    document.getElementById("Email").value = "";
    document.getElementById("Phone").value = "";

}

function CloseModalAddUser() {

    $('#ModalAddUser').modal('hide')

}

function ShowModalAddAddress() {

    $('#ModalAddAddress').modal('show')
    document.getElementById("NameAddress").value = "";
    document.getElementById("Street").value = "";
    document.getElementById("Ext").value = "";
    document.getElementById("CP").value = "";
    document.getElementById("Colony").value = "";
    document.getElementById("City").value = "";
    document.getElementById("Country").value = "";

}

function CloseModalAddAddress() {

    $('#ModalAddAddress').modal('hide')

}

function ShowModalAddOV() {

    $('#ModalAddOV').modal('show')

}

function CloseModalAddOV() {

    $('#ModalAddOV').modal('hide')

}

function ShowModalFindOV() {

    $('#ModalFindOV').modal('show')
    document.getElementById("OVText").value = "";

}

function CloseModalFindOV() {

    $('#ModalFindOV').modal('hide')

}

function CloseModalEditUser() {

    $('#ModaEditUser').modal('hide')

}

function CloseModalEditAddress() {

    $('#ModaEditAddres').modal('hide')

}

function CloseModalEditRoute() {

    $('#ModaEditRoute').modal('hide')

}

const AddUser = () => {

    const Name = document.getElementById('Name').value;
    const Lastname = document.getElementById('Lastname').value;
    const Username = document.getElementById('Username').value;
    const Password = document.getElementById('Password').value;
    const Email = document.getElementById('Email').value;
    const Phone = document.getElementById('Phone').value;

    const Id_Role = document.getElementById('Id_Role').value;
    const ChooseAddres = document.getElementById('ChooseAddres').value;

    const regexCorreo = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$/;
    const regexPhone = /^\(?(\d{3})\)?[-]?(\d{3})[-]?(\d{4})$/;
 
    resetFormAddUser();
    //Verificar que los campos no esten vacios y que sea un correo valido 
    if (Name == "" || Lastname == "" || Username == "" || Password == "" || Email == "" || Phone == "") { 
        myMessage('info', 'Favor de completar el formulario')
        // debugger
        if(Name == null || Name == "") invalidFeedbackForm('Name', 'invalidFeedback-Form-AddUser-Name', 'Por favor, ingresar un nombre(s).'); 
        if(Lastname == null || Lastname == "") invalidFeedbackForm('Lastname', 'invalidFeedback-Form-AddUser-Lastname', 'Por favor, ingresar el apellido(s).'); 
        if(Username == null || Username == "") invalidFeedbackForm('Username', 'invalidFeedback-Form-AddUser-Username', 'Por favor, ingresar un nombre de usuario.');   
        if(Password == null || Password == '') invalidFeedbackForm('Password', 'invalidFeedback-Form-AddUser-Password', 'Por favor, ingresar una contraseña.');
        if(Email == null || Email == '') invalidFeedbackForm('Email', 'invalidFeedback-Form-AddUser-Email', 'Por favor, ingresar un correo.'); 
        if(!regexCorreo.test(Email) && Email != null && Email != '') invalidFeedbackForm('Email', 'invalidFeedback-Form-AddUser-Email', 'Correo invalido.');
        if(Phone == null || Phone == '') invalidFeedbackForm('Phone', 'invalidFeedback-Form-AddUser-Phone', 'Por favor, ingresar un numero de telefono.'); 
        if(!regexPhone.test(Phone) && Phone != null && Phone != '') invalidFeedbackForm('Phone', 'invalidFeedback-Form-AddUser-Phone', 'Numero de telefono valido.'); 
       
        return
    } 

    $.ajax({

        url: '/Home/AddUser',
        data: JSON.stringify({ Name: Name, Lastname: Lastname, Username: Username, Password: Password, Email: Email, Phone: Phone, Id_Role: Id_Role, ChooseAddres: ChooseAddres }),
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data1) {

            console.log(data1)

            if (data1.Name != null) {

                $.ajax({

                    url: '/Home/Users',
                    //data: JSON.stringify({ UsuarioPar: usr, PswPar: pwd }),
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data1) {

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
                            title: 'Registro guardado',
                            text: ""
                        })

                        //$('#Data-Usuarios').bootstrapTable({ data: data1 })
                        $('#Data-Usuarios').bootstrapTable('refreshOptions', { data: data1 })
                        CloseModalAddUser();
                    }

                });

                //$('#Data-Usuarios').bootstrapTable('refreshOptions', { data: data1 })

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
                    title: 'No se pudo agregar el usuario',
                    text: ""
                })

                //$('#Data-Usuarios').bootstrapTable('refreshOptions', { data: data1 })
                //CerrarModales();

            }


        }
    });
}

const EditUser = () => {

    const IdEdit = document.getElementById('IdEdit').value;
    const NameEdit = document.getElementById('NameEdit').value;
    const LastnameEdit = document.getElementById('LastnameEdit').value;
    const UsernameEdit = document.getElementById('UsernameEdit').value;
    const PasswordEdit = document.getElementById('PasswordEdit').value;
    const EmailEdit = document.getElementById('EmailEdit').value;
    const PhoneEdit = document.getElementById('PhoneEdit').value;
    const EnableEdit = document.getElementById('EnableEdit').value;
    const Id_RoleEdit = document.getElementById('Id_RoleEdit').value;
    const ChooseAddresEdit = document.getElementById('ChooseAddresEdit').value;

    if (NameEdit == "" || LastnameEdit == "" || UsernameEdit == "" || PasswordEdit == "" || EmailEdit == "" || PhoneEdit == "") {
        Swal.fire({
            icon: 'warning',
            title: 'Es nesesario llenar todos los campos',
            text: '',

        })
        return
    }

    //console.log(IdEdit)
    //console.log(NameEdit)
    //console.log(LastnameEdit)
    //console.log(UsernameEdit)
    //console.log(PasswordEdit)
    //console.log(EmailEdit)
    //console.log(PhoneEdit)
    //console.log(EnableEdit)
    //console.log(Id_RoleEdit)
    //console.log(ChooseAddresEdit)
    //return  

    $.ajax({

        url: '/Home/EditUser',
        data: JSON.stringify({
            IdEdit: IdEdit, NameEdit: NameEdit, LastnameEdit: LastnameEdit, UsernameEdit: UsernameEdit, PasswordEdit: PasswordEdit, EmailEdit: EmailEdit, PhoneEdit: PhoneEdit, EnableEdit: EnableEdit, Id_RoleEdit: Id_RoleEdit, ChooseAddresEdit: ChooseAddresEdit,
        }),
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data1) {

            if (data1 = true) {

                $.ajax({

                    url: '/Home/Users',
                    //data: JSON.stringify({ UsuarioPar: usr, PswPar: pwd }),
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data1) {


                        //$('#Data-Usuarios').bootstrapTable({ data: data1 })
                        $('#Data-Usuarios').bootstrapTable('refreshOptions', { data: data1 })
                        CloseModalEditUser();
                    }

                });
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
                    title: 'No se pudo editar el usuario',
                    text: ""
                })

            }

        }
    });
}

const DisableUser = (Id) => {

    $.ajax({

        url: '/Home/DisableUser',
        data: JSON.stringify({ Id: Id }),
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data1) {

            if (data1 == true) {

                $.ajax({

                    url: '/Home/Users',
                    //data: JSON.stringify({ UsuarioPar: usr, PswPar: pwd }),
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data1) {

                        //$('#Data-Usuarios').bootstrapTable({ data: data1 })
                        $('#Data-Usuarios').bootstrapTable('refreshOptions', { data: data1 })

                    }

                });

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
                    title: 'No se pudo Eliminar el usuario',
                    text: ""
                })

            }

        }
    });
}

const DeleteUser = (Id) => {

    $.ajax({

        url: '/Home/DeleteUser',
        data: JSON.stringify({ Id: Id }),
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data1) {

            if (data1 == true) {

                $.ajax({

                    url: '/Home/Users',
                    //data: JSON.stringify({ UsuarioPar: usr, PswPar: pwd }),
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data1) {

                        //$('#Data-Usuarios').bootstrapTable({ data: data1 })
                        $('#Data-Usuarios').bootstrapTable('refreshOptions', { data: data1 })

                    }

                });

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
                    title: 'No se pudo Eliminar el usuario',
                    text: ""
                })

            }

        }
    });
}

const DisableAddress = (Id) => {

    $.ajax({

        url: '/Home/DisableAddress',
        data: JSON.stringify({ Id: Id }),
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data1) {

            if (data1 == true) {

                $.ajax({

                    url: '/Home/Adress',
                    //data: JSON.stringify({ UsuarioPar: usr, PswPar: pwd }),
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data1) {

                        //$('#Data-Usuarios').bootstrapTable({ data: data1 })
                        $('#Data-Adress').bootstrapTable('refreshOptions', { data: data1 })

                    }

                });

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
                    title: 'No se pudo Eliminar el usuario',
                    text: ""
                })

            }

        }
    });
}

const AddAddress = () => {

    const NameAddress = document.getElementById('NameAddress').value;
    const Street = document.getElementById('Street').value;
    const Ext = document.getElementById('Ext').value;
    const CP = document.getElementById('CP').value;
    const Colony = document.getElementById('Colony').value;
    const City = document.getElementById('City').value;
    const Country = document.getElementById('Country').value;

    if (NameAddress == "" || Street == "" || Ext == "" || CP == "" || Colony == "" || City == "" || Country == "") {
        Swal.fire({
            icon: 'warning',
            title: 'Es nesesario llenar todos los campos',
            text: '',

        })
        return
    }

    $.ajax({

        url: '/Home/AddAddress',
        data: JSON.stringify({
            NameAddress: NameAddress, Street: Street, Ext: Ext, CP: CP, Colony: Colony, City: City, Country: Country
        }),
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data1) {

            console.log(data1)

            if (data1 == true) {

                $.ajax({

                    url: '/Home/Address',
                    //data: JSON.stringify({ UsuarioPar: usr, PswPar: pwd }),
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data1) {

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
                            title: 'Registro guardado',
                            text: ""
                        })

                        //$('#Data-Usuarios').bootstrapTable({ data: data1 })
                        $('#Data-Adress').bootstrapTable('refreshOptions', { data: data1 })
                        CloseModalAddAddress();
                    }

                });

                //$('#Data-Usuarios').bootstrapTable('refreshOptions', { data: data1 })

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
                    title: 'No se pudo agregar el usuario',
                    text: ""
                })

                //$('#Data-Usuarios').bootstrapTable('refreshOptions', { data: data1 })
                //CerrarModales();

            }


        }
    });
}

const EditAddress = () => {

    const IDAddresEdit = document.getElementById('IDAddresEdit').value;
    const NameAddressEdit = document.getElementById('NameAddressEdit').value;
    const StreetEdit = document.getElementById('StreetEdit').value;
    const ExtEdit = document.getElementById('ExtEdit').value;
    const CPEdit = document.getElementById('CPEdit').value;
    const ColonyEdit = document.getElementById('ColonyEdit').value;
    const CityEdit = document.getElementById('CityEdit').value;
    const CountryEdit = document.getElementById('CountryEdit').value;
    const EnableEdit = document.getElementById('EnableEdit').value;

    if (NameAddressEdit == "" || StreetEdit == "" || ExtEdit == "" || CPEdit == "" || ColonyEdit == "" || CityEdit == "" || CountryEdit == "") {
        Swal.fire({
            icon: 'warning',
            title: 'Es nesesario llenar todos los campos',
            text: '',

        })
        return
    }

    //console.log(IdEdit)
    //console.log(NameEdit)
    //console.log(LastnameEdit)
    //console.log(UsernameEdit)
    //console.log(PasswordEdit)
    //console.log(EmailEdit)
    //console.log(PhoneEdit)
    //console.log(EnableEdit)
    //console.log(Id_RoleEdit)
    //console.log(ChooseAddresEdit)
    //return  

    $.ajax({

        url: '/Home/EditAddress',
        data: JSON.stringify({
            IDAddresEdit: IDAddresEdit, NameAddressEdit: NameAddressEdit, StreetEdit: StreetEdit, ExtEdit: ExtEdit, CPEdit: CPEdit, ColonyEdit: ColonyEdit, CityEdit: CityEdit, CountryEdit: CountryEdit, EnableEdit: EnableEdit
        }),
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data1) {

            if (data1 = true) {

                $.ajax({

                    url: '/Home/Address',
                    //data: JSON.stringify({ UsuarioPar: usr, PswPar: pwd }),
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data1) {


                        //$('#Data-Usuarios').bootstrapTable({ data: data1 })
                        $('#Data-Adress').bootstrapTable('refreshOptions', { data: data1 })
                        CloseModalEditAddress();
                    }

                });
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
                    title: 'No se pudo editar el usuario',
                    text: ""
                })

            }

        }
    });
}

const EditRoute = () => {

    const IDRouteEdit = document.getElementById('IDRouteEdit').value;
    const ShipToCodeRouteEdit = document.getElementById('ShipToCodeRouteEdit').value;
    const CardNameRouteEdit = document.getElementById('CardNameRouteEdit').value;
    const StreetRouteEdit = document.getElementById('StreetRouteEdit').value;
    const BlockRouteEdit = document.getElementById('BlockRouteEdit').value;
    const ZipCodeRouteEdit = document.getElementById('ZipCodeRouteEdit').value;
    const CityRouteEdit = document.getElementById('CityRouteEdit').value;
    const U_NAMERouteEdit = document.getElementById('U_NAMERouteEdit').value;
    const ConditionRouteEdit = document.getElementById('ConditionRouteEdit').value;
    const DocNumsRouteEdit = document.getElementById('DocNumsRouteEdit').value;
    const CommentsRouteEdit = document.getElementById('CommentsRouteEdit').value;
    const PhoneRouteEdit = document.getElementById('PhoneRouteEdit').value;

    // Buscar si ya existe una fila con el mismo ID
    var table = document.getElementById('Data-Route');
    var rows = table.getElementsByTagName('tr');

    // Obtener referencia a la tabla Bootstrap
    var $table = $('#Data-Route');

    // Obtener los datos de la tabla
    var tableData = $table.bootstrapTable('getData');

    // tableData ahora contiene los datos de tu tabla
    console.log(tableData);

    //Variable validacion
    var found = false;

    tableData.forEach(function (number) {

        console.log(number); // Imprime cada número en la consola

        if (number.Id == IDRouteEdit) {
            console.log("Encontro id");
            //Asignamos valores
            number.ShipToCode = ShipToCodeRouteEdit;
            number.CardName = CardNameRouteEdit;
            number.Street = StreetRouteEdit;
            number.Block = BlockRouteEdit;
            number.ZipCode = ZipCodeRouteEdit;
            number.City = CityRouteEdit;
            number.U_NAME = U_NAMERouteEdit;
            number.Condition = ConditionRouteEdit;
            number.DocNums = DocNumsRouteEdit;
            number.Comments = CommentsRouteEdit;
            number.Phone = PhoneRouteEdit;

            found = true;
            return; // Sale de la función de devolución de llamada
        }
    });

    // Si necesitas hacer algo después del bucle, puedes verificar si se encontró el elemento
    if (found) {
        console.log("actualizacion de tabla"); // Imprime cada número en la consola
        console.log(tableData);
        $('#Data-Route').bootstrapTable('refreshOptions', { data: tableData })
    }

    // Limpiar campos del modal
    document.getElementById('IDRouteEdit').value = '';
    document.getElementById('ShipToCodeRouteEdit').value = '';
    document.getElementById('CardNameRouteEdit').value = '';
    document.getElementById('StreetRouteEdit').value = '';
    document.getElementById('BlockRouteEdit').value = '';
    document.getElementById('ZipCodeRouteEdit').value = '';
    document.getElementById('CityRouteEdit').value = '';
    document.getElementById('U_NAMERouteEdit').value = '';
    document.getElementById('ConditionRouteEdit').value = '';
    document.getElementById('DocNumsRouteEdit').value = '';
    document.getElementById('CommentsRouteEdit').value = '';
    document.getElementById('PhoneRouteEdit').value = '';

    // Cerrar el modal
    $('#ModaEditRoute').modal('hide')

    return
    //var existingRow = null;

    //for (var i = 0; i < rows.length; i++) {
    //    var cells = rows[i].getElementsByTagName('td');
    //    if (cells.length > 0 && cells[0].textContent === elementId) {
    //        existingRow = rows[i];
    //        break;
    //    }
    //}

    // Si ya existe una fila con el mismo ID, actualizarla
    /*if (existingRow) {*/
    //    existingRow.cells[1].textContent = elementName; // Actualizar el nombre
    //} else {
    //    // Si no existe una fila con el mismo ID, crear una nueva fila
    //    var newRow = table.insertRow();
    //    newRow.insertCell(0).appendChild(document.createTextNode(elementId));
    //    newRow.insertCell(1).appendChild(document.createTextNode(elementName));
    //}

    //rows[IDRouteEdit].cells[1].textContent = ShipToCodeRouteEdit; // Actualizar ShipToCode
    //rows[IDRouteEdit].cells[2].textContent = CardNameRouteEdit; // Actualizar CardName
    //rows[IDRouteEdit].cells[3].textContent = StreetRouteEdit; // Actualizar Street
    //rows[IDRouteEdit].cells[4].textContent = BlockRouteEdit; // Actualizar Block
    //rows[IDRouteEdit].cells[5].textContent = ZipCodeRouteEdit; // Actualizar ZipCode
    //rows[IDRouteEdit].cells[6].textContent = CityRouteEdit; // Actualizar City
    //rows[IDRouteEdit].cells[7].textContent = U_NAMERouteEdit; // Actualizar U_NAME
    //rows[IDRouteEdit].cells[8].textContent = ConditionRouteEdit; // Actualizar Condition
    //rows[IDRouteEdit].cells[9].textContent = DocNumsRouteEdit; // Actualizar DocNums
    //rows[IDRouteEdit].cells[10].textContent = CommentsRouteEdit; // Actualizar Comments
    //rows[IDRouteEdit].cells[11].textContent = PhoneRouteEdit; // Actualizar Phone

    //// Limpiar campos del modal
    //document.getElementById('IDRouteEdit').value = '';
    //document.getElementById('ShipToCodeRouteEdit').value = '';
    //document.getElementById('CardNameRouteEdit').value = '';
    //document.getElementById('StreetRouteEdit').value = '';
    //document.getElementById('BlockRouteEdit').value = '';
    //document.getElementById('ZipCodeRouteEdit').value = '';
    //document.getElementById('CityRouteEdit').value = '';
    //document.getElementById('U_NAMERouteEdit').value = '';
    //document.getElementById('ConditionRouteEdit').value = '';
    //document.getElementById('DocNumsRouteEdit').value = '';
    //document.getElementById('CommentsRouteEdit').value = '';
    //document.getElementById('PhoneRouteEdit').value = '';

    //// Cerrar el modal
    //$('#ModaEditRoute').modal('hide')

}

const FindRouteOV = () => {

    const Orden = document.getElementById('OVText').value;

    if (Orden == "") {
        Swal.fire({
            icon: 'warning',
            title: 'Es necesario introducir el número de folio de la orden de venta',
            text: '',

        })
        return
    }

    $.ajax({

        url: '/Home/OVRouteMCDMX',
        data: JSON.stringify({ OvText: Orden }),
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data1) {

            console.log(data1)

            if (data1.Id == 0) {

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
                    title: 'No se encontraron resultados',
                    text: ""
                })

                return

            }

            CloseModalFindOV();

            ShowModalAddOV();


            document.getElementById("ShipToCodeAddOv").value = data1.ShipToCode;
            document.getElementById("CardNameAddOv").value = data1.CardName;
            document.getElementById("StreetAddOv").value = data1.Street;
            document.getElementById("BlockAddOv").value = data1.Block;
            document.getElementById("ZipCodeAddOv").value = data1.ZipCode;
            document.getElementById("CityAddOv").value = data1.City;
            document.getElementById("U_NAMEAddOv").value = data1.U_NAME;
            document.getElementById("ConditionAddOv").value = data1.Condition;
            document.getElementById("DocNumsAddOv").value = data1.DocNums;
            document.getElementById("DocDateAddOv").value = data1.DocDate;
            document.getElementById("RouteAddOv").value = data1.Route;

        }

    });

}

const AddOvToROute = () => {

    const ShipToCode = document.getElementById("ShipToCodeAddOv").value;
    const CardName = document.getElementById("CardNameAddOv").value;
    const Street = document.getElementById("StreetAddOv").value;
    const Block = document.getElementById("BlockAddOv").value;
    const ZipCode = document.getElementById("ZipCodeAddOv").value;
    const City = document.getElementById("CityAddOv").value;
    const U_NAME = document.getElementById("U_NAMEAddOv").value;
    const Condition = document.getElementById("ConditionAddOv").value;
    const DocNums = document.getElementById("DocNumsAddOv").value;
    const Comments = document.getElementById("CommentsAddOv").value;
    const Phone = document.getElementById("PhoneAddOv").value;

    CloseModalAddOV();

    // Obtener referencia a la tabla Bootstrap
    var $table = $('#Data-Route');

    // Obtener los datos de la tabla
    var tableData = $table.bootstrapTable('getData');

    // tableData ahora contiene los datos de tu tabla
    //console.log(tableData);

    // Suponiendo que 'tabla' es tu arreglo de objetos
    var ultimoRegistro = tableData[tableData.length - 1]; // Obtiene el último registro

    // Si deseas obtener solo el ID del último registro
    var ultimoID = ultimoRegistro.Id;

    //console.log(ultimoID +1)

    // Nuevo objeto a agregar
    var nuevoElemento = {
        "Id": 23,
        "ShipToCode": ShipToCode,
        "CardName": CardName,
        "Street": Street,
        "Block": Block,
        "ZipCode": ZipCode,
        "City": City,
        "U_NAME": U_NAME,
        "Condition": Condition,
        "DocNums": DocNums,
        "Comments": Comments,
        "Phone": Phone
    };

    // Agregar el nuevo elemento a la tabla
    tableData.push(nuevoElemento);

    // Ahora la tabla tiene un nuevo elemento agregado
    console.log(tableData);

    $('#Data-Route').bootstrapTable('refreshOptions', { data: tableData })

}

const SaveRoute = () => {

    var r = route
    //return

    // Obtener referencia a la tabla Bootstrap
    var $table = $('#Data-Route');

    // Obtener los datos de la tabla
    var tableData = $table.bootstrapTable('getData');

    // tableData ahora contiene los datos de tu tabla
    console.log(tableData);


    $.ajax({

        url: '/Home/SaveRoute',
        data: JSON.stringify({ Route: tableData, laruta: r }),
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data1) {

            console.log(data1)

            if (data1 == false) {

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
                    title: 'Hubo un error interno en el guardado',
                    text: ""
                })

                return

            }
            else if (data1 == true) {

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
                    title: 'Ruta guardada',
                    text: ""
                })

                $('#Data-Route').bootstrapTable('removeAll');
                return

            }
            else if (data1 == "login") {
                setTimeout(() => window.location.href = '/Auth/Signin', 2000)
            }else if (data1 == "RutaG") {
                myMessage('error', 'La ruta ya habia sido guardada previamente');
                return
            }


        }

    });

}

document.getElementById('toggle-multiple-collapse').addEventListener('click', function () {
    var sidebarAuth1 = document.getElementById('sidebar-auth');
    var sidebarAuth2 = document.getElementById('sidebar-auth2');

    if (sidebarAuth1.classList.contains('show') || sidebarAuth2.classList.contains('show')) {
        sidebarAuth1.classList.remove('show');
        sidebarAuth2.classList.remove('show');
    } else {
        sidebarAuth1.classList.add('show');
        sidebarAuth2.classList.add('show');
    }
});

const CleanSelect = (select_id) => { 

    var selectAdressUser = document.getElementById(select_id);
    // var selectAdressUser = document.getElementById('ChooseAddres');
 
        if (selectAdressUser.selectedIndex != -1){
          //Limpiar el SELECT LOTES
          var length = selectAdressUser.options.length;
          for (i = length-1; i >= 0; i--) {
            selectAdressUser.options[i] = null;
          }
        } 
}

const CheckShowPassword = () => {
    console.log("CheckShowPassword");

    var passwordInput = document.getElementById("Password");
    var toggleCheckbox = document.getElementById("checkshowpasswordAdduser");
    
    if (toggleCheckbox.checked) {
        passwordInput.type = "text";
    } else {
        passwordInput.type = "password";
    }
}

const myMessage = (icon, title) => {
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
        icon: icon,
        title: title,
        text: ""
    })

}

const invalidFeedbackForm = (id, feedback_id, feedbackText) => {

    console.log(id);
    console.log(feedback_id);
    var input = document.getElementById(id);
    var invalidFeedback = document.getElementById(feedback_id);
 
    input.classList.add('is-invalid');
    invalidFeedback.innerText = feedbackText;
    // input.style.border = '1px solid #eee ';
}

const resetFormAddUser = () => {

    document.getElementById('Name').classList.remove('is-invalid');
    document.getElementById('Lastname').classList.remove('is-invalid');;
    document.getElementById('Username').classList.remove('is-invalid');;
    document.getElementById('Password').classList.remove('is-invalid');;
    document.getElementById('Email').classList.remove('is-invalid');;
    document.getElementById('Phone').classList.remove('is-invalid');;

}

window.operateEventUsersEnable = { 
};

function OpcionesUserEnable(value, row, index) {
    if(value == true){
    return [
        ` <h5> <span class="badge rounded-pill text-bg-primary">Activo</span> </h5>`,  
    ].join('')
}else{
    return [
        ` <h5> <span class="badge rounded-pill text-bg-danger">Desactivado</span> </h5>`,  
    ].join('')
}

}