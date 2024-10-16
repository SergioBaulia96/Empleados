window.onload = ListadoInformesTres();

function ListadoInformesTres() {

    $.ajax({
        url: '../../Empleados/ListadoInformesTres',
        data: {},
        type: 'POST',
        dataType: 'json',
        success: function (sectoresMostrar) {

            let contenidoTabla = ``;

            $.each(sectoresMostrar, function (index, sector) {

                contenidoTabla += `
                <tr>
                    <td>${sector.nombreSector}</td>
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

                $.each(sector.listadoLocalidades, function (index, localidad) {
                    contenidoTabla += `
                        <tr>
                            <td></td>
                            <td>${localidad.nombreLocalidad}</td>
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
                        contenidoTabla += `
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

            });

            document.getElementById("tbody-informesectores").innerHTML = contenidoTabla;
        },

        error: function (xhr, status) {
            console.log('Disculpe, existió un problema al cargar el listado');
        }
    });
}