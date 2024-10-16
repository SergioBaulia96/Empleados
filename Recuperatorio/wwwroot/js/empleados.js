window.onload = ListadoDeEmpleados();

function ListadoDeEmpleados()
{
    $.ajax({
        url: '../../Empleados/ListadoDeEmpleados',
        data: {},
        type: 'POST',
        dataType: 'json',
        success: function(empleadosMostrar){
            $("#empleadosModal").modal("hide");
            LimpiarModal();
            
            let tabla = ``

            $.each(empleadosMostrar, function(index, empleados){

                tabla += `
                <tr>
                    <td>${empleados.nombre}</td>
                    <td>${empleados.apellido}</td>
                    <td>${empleados.nombreLocalidad}</td>
                    <td>${empleados.nombreSector}</td>
                    <td>${empleados.direccion}</td>
                    <td>${empleados.nacimiento}</td>
                    <td>${empleados.telefono}</td>
                    <td>${empleados.email}</td>
                    <td>${empleados.salario}</td>
                    <td class="text-center">
                    <button type="button" class="btn btn-success btn-sm" onclick="ModalEditar(${empleados.empleadoID})">
                    Editar
                    </button>
                    </td>
                    <td class="text-center">
                    <button type="button" class="btn btn-danger btn-sm" onclick="ValidarEliminacion(${empleados.empleadoID})">
                    Eliminar
                    </button>
                    </td>
                </tr>
                `;
            });
            document.getElementById("tbody-empleados").innerHTML = tabla;                                
        },
        error: function(xhr, status){
            console.log('Problemas al cargar la tabla');
        }
    });
}

function LimpiarModal(){
    document.getElementById("EmpleadoID").value = 0;
    document.getElementById("LocalidadID").value = 0;
    document.getElementById("SectorID").value = 0;
    document.getElementById("Nombre").value = "";
    document.getElementById("Apellido").value = "";
    document.getElementById("Direccion").value = "";
    document.getElementById("Nacimiento").value = "";
    document.getElementById("Telefono").value = "";
    document.getElementById("Email").value = "";
    document.getElementById("Salario").value = "";
}

function NuevoEmpleado(){
    $("#tituloModal").text("Nuevo Empleado");
}

function GuardarEmpleado(){
    let empleadoID = document.getElementById("EmpleadoID").value;
    let localidadID = document.getElementById("LocalidadID").value;
    let sectorID = document.getElementById("SectorID").value;
    let nombre = document.getElementById("Nombre").value;
    let apellido = document.getElementById("Apellido").value;
    let direccion = document.getElementById("Direccion").value;
    let nacimiento = document.getElementById("Nacimiento").value;
    let telefono = document.getElementById("Telefono").value;
    let email = document.getElementById("Email").value;
    let salario = document.getElementById("Salario").value;

    $.ajax({
        url: '../../Empleados/GuardarEmpleado',
        data: {
            empleadoID: empleadoID,
            localidadID : localidadID,
            sectorID : sectorID,
            nombre: nombre,
            apellido: apellido,
            direccion: direccion,
            nacimiento: nacimiento,
            telefono: telefono,
            email: email,
            salario: salario
        },
        type: 'POST',
        dataType: 'json',
        success: function(resultado){
            if(resultado != "") {
                alert(resultado)
            }
            ListadoDeEmpleados();
        },
        error: function(xhr, status){
            console.log('Problemas al guardar el empleado');
        },
    });
}

function ModalEditar(empleadoID){
    $.ajax({
        url: '../../Empleados/ListadoDeEmpleados',
        data: { empleadoID: empleadoID },
        type: 'POST',
        dataType: 'json',
        success: function(listadoEmpleados){
            listadoEmpleado = listadoEmpleados[0];
            
            document.getElementById("EmpleadoID").value = empleadoID
            $("#tituloModal").text("Editar Empleado");
            document.getElementById("LocalidadID").value = listadoEmpleado.localidadID
            document.getElementById("SectorID").value = listadoEmpleado.sectorID
            document.getElementById("Nombre").value = listadoEmpleado.nombre;
            document.getElementById("Apellido").value = listadoEmpleado.apellido;
            document.getElementById("Direccion").value = listadoEmpleado.direccion;
            document.getElementById("Nacimiento").value = listadoEmpleado.nacimiento;
            document.getElementById("Telefono").value = listadoEmpleado.telefono;
            document.getElementById("Email").value = listadoEmpleado.email;
            document.getElementById("Salario").value = listadoEmpleado.salario;
            $("#empleadosModal").modal("show");
        },
        error: function(xhr, status){
            console.log('Problemas al cargar el empleado');
        }
    });
}

function ValidarEliminacion(empleadoID)
{
    var elimina = confirm("¿Esta seguro que desea eliminar?");
    if(elimina == true)
        {
            EliminarEmpleado(empleadoID);
        }
}

function EliminarEmpleado(empleadoID){
    $.ajax({
        url: '../../Empleados/EliminarEmpleado',
        data: { empleadoID: empleadoID },
        type: 'POST',
        dataType: 'json',
        success: function(EliminarEmpleado){
            ListadoDeEmpleados()
        },
        error: function(xhr, status){
            console.log('Problemas al eliminar el empleado');
        }
    });
}