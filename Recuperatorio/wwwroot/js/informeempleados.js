window.onload = ListadoInformes();

function ListadoInformes() {
    let buscarLocalidad = document.getElementById("BuscarLocalidad").value;

    $.ajax({
        url: '../../Empleados/ListadoInformes',
        data: {
            BuscarLocalidad : buscarLocalidad
        },
        type: 'POST',
        dataType: 'json',
        success: function (localidadesMostrar){

            let contenidoTabla = ``;

            $.each(localidadesMostrar, function (index, localidad) {
                
                contenidoTabla += `
                <tr>
                    <td>${localidad.nombreLocalidad}</td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                `;

                $.each(localidad.listadoEmpleados, function (index, empleado) {
                    contenidoTabla +=` 
                    <tr>
                        <td></td>
                        <td></td>
                        <td>${empleado.nombre}</td>
                        <td>${empleado.apellido}</td>
                        <td>${empleado.direccion}</td>
                        <td>${empleado.nacimiento}</td>
                        <td>${empleado.telefono}</td>
                        <td>${empleado.email}</td>
                        <td>${empleado.salario}</td>
                    </tr>
                    `;
                });
        });
        document.getElementById("tbody-informeempleados").innerHTML = contenidoTabla;
    },
    error: function (xhr, status) {
        alert('Disculpe, existió un problema al procesar la solicitud.');
    }
    });
}

