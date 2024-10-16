window.onload = ListadoLocalidades();

function ListadoLocalidades()
{
    $.ajax({
        url: '../../Localidades/ListadoLocalidades',
        data: {},
        type: 'POST',
        dataType: 'json',
        success: function(listadoLocalidades){
            $("#localidadModal").modal("hide");
            LimpiarModal();
            
            let tabla = ``

            $.each(listadoLocalidades, function(index, localidades){

                tabla += `
                <tr>
                    <td>${localidades.nombreLocalidad}</td>
                    <td class="text-center">
                    <button type="button" class="btn btn-success btn-sm" onclick="ModalEditar(${localidades.localidadID})">
                    Editar
                    </button>
                    </td>
                    <td class="text-center">
                    <button type="button" class="btn btn-danger btn-sm" onclick="ValidarEliminacion(${localidades.localidadID})">
                    Eliminar
                    </button>
                    </td>
                </tr>
                `;
            });
            document.getElementById("tbody-localidades").innerHTML = tabla;                                
        },
        error: function(xhr, status){
            console.log('Problemas al cargar la tabla');
        }
    });
}

function LimpiarModal(){
    document.getElementById("LocalidadID").value = 0;
    document.getElementById("NombreLocalidad").value = "";
}

function NuevaLocalidad(){
    $("#tituloModal").text("Nueva Localidad");
}

function GuardarLocalidad(){
    let localidadID = document.getElementById("LocalidadID").value;
    let nombreLocalidad = document.getElementById("NombreLocalidad").value;
    $.ajax({
        url: '../../Localidades/GuardarLocalidad',
        data: {
            localidadID : localidadID,
            nombreLocalidad : nombreLocalidad
        },
        type: 'POST',
        dataType: 'json',
        success: function(resultado){
            if(resultado != "") {
                alert(resultado)
            }
            ListadoLocalidades();
        },
        error: function(xhr, status){
            console.log('Problemas al guardar Localidad');
        },
    });
}

function ModalEditar(localidadID){
    $.ajax({
        url: '../../Localidades/ListadoLocalidades',
        data: { localidadID: localidadID },
        type: 'POST',
        dataType: 'json',
        success: function(localidadPorID){
            let localidad = localidadPorID[0];
            
            document.getElementById("LocalidadID").value = localidadID
            $("#tituloModal").text("Editar Empleado");
            document.getElementById("NombreLocalidad").value = localidad.nombreLocalidad;
            $("#localidadModal").modal("show");
        },
        error: function(xhr, status){
            console.log('Problemas al cargar el empleado');
        }
    });
}

function ValidarEliminacion(localidadID)
{
    var elimina = confirm("¿Esta seguro que desea eliminar?");
    if(elimina == true)
        {
            EliminarLocalidad(localidadID);
        }
}

function EliminarLocalidad(localidadID){
    $.ajax({
        url: '../../Localidades/EliminarLocalidad',
        data: { localidadID: localidadID },
        type: 'POST',
        dataType: 'json',
        success: function(EliminarLocalidad){
            ListadoLocalidades()
        },
        error: function(xhr, status){
            console.log('Problemas al eliminar Localidad');
        }
    });
}