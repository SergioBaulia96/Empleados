window.onload = ListadoSectores();

function ListadoSectores()
{
    $.ajax({
        url: '../../Sectores/ListadoSectores',
        data: {},
        type: 'POST',
        dataType: 'json',
        success: function(ListadoSectores){
            $("#sectorModal").modal("hide");
            LimpiarModal();
            
            let tabla = ``

            $.each(ListadoSectores, function(index, sectores){

                tabla += `
                <tr>
                    <td>${sectores.nombreSector}</td>
                    <td class="text-center">
                    <button type="button" class="btn btn-success btn-sm" onclick="ModalEditar(${sectores.sectorID})">
                    Editar
                    </button>
                    </td>
                    <td class="text-center">
                    <button type="button" class="btn btn-danger btn-sm" onclick="ValidarEliminacion(${sectores.sectorID})">
                    Eliminar
                    </button>
                    </td>
                </tr>
                `;
            });
            document.getElementById("tbody-sectores").innerHTML = tabla;                                
        },
        error: function(xhr, status){
            console.log('Problemas al cargar la tabla');
        }
    });
}

function LimpiarModal(){
    document.getElementById("SectorID").value = 0;
    document.getElementById("NombreSector").value = "";
}

function NuevoSector(){
    $("#tituloModal").text("Nueva Localidad");
}

function GuardarSector(){
    let sectorID = document.getElementById("SectorID").value;
    let nombreSector = document.getElementById("NombreSector").value;
    $.ajax({
        url: '../../Sectores/GuardarSector',
        data: {
            sectorID : sectorID,
            nombreSector : nombreSector
        },
        type: 'POST',
        dataType: 'json',
        success: function(resultado){
            if(resultado != "") {
                alert(resultado)
            }
            ListadoSectores();
        },
        error: function(xhr, status){
            console.log('Problemas al guardar Localidad');
        },
    });
}

function ModalEditar(SectorID){
    $.ajax({
        url: '../../Sectores/ListadoSectores',
        data: { sectorID: SectorID },
        type: 'POST',
        dataType: 'json',
        success: function(sectoresPorID){
            let sector = sectoresPorID[0];
            
            document.getElementById("SectorID").value = SectorID
            $("#tituloModal").text("Editar sector");
            document.getElementById("NombreSector").value = sector.nombreSector;
            $("#sectorModal").modal("show");
        },
        error: function(xhr, status){
            console.log('Problemas al cargar el empleado');
        }
    });
}

function ValidarEliminacion(SectorID)
{
    var elimina = confirm("¿Esta seguro que desea eliminar?");
    if(elimina == true)
        {
            EliminarSector(SectorID);
        }
}

function EliminarSector(SectorID){
    $.ajax({
        url: '../../Sectores/EliminarSector',
        data: { sectorID: SectorID },
        type: 'POST',
        dataType: 'json',
        success: function(EliminarSector){
            ListadoSectores()
        },
        error: function(xhr, status){
            console.log('Problemas al eliminar Localidad');
        }
    });
}