

window.onload = () => {
    loadData();
}

function loadData () {
    fetch( "https://localhost:44366/api/employee" )
        .then( response => response.json() )
        .then( fillTableEmployees );
}

function fillTableEmployees ( data ) {
    let grid = document.getElementById( "employee-table" );
    let table = "<tr><th></th><th>Nombre</th><th>Apellido</th><th>Posición</th><th>Salario</th></tr>";

    for ( let i in data ) {
        table += "<tr onclick='selectRowForEditorDelete()'>";
        table += "<td style='displya:none'>" + data[i].id + "</td>";
        table += "<td>" + data[i].name + "</td>";
        table += "<td>" + data[i].surname + "</td>";
        table += "<td>" + data[i].position + "</td>";
        table += "<td>" + data[i].salary + "</td>";
        table += "</tr>";
    }

    grid.innerHTML = table;
}

function addEmployee () {
    const formEmp = document.getElementById( "employee-data" );
    formEmp.onsubmit = ( e ) => {
        e.preventDefault();
        const input = JSON.stringify( {
            "name": document.getElementById( "name" ).value,
            "surname": document.getElementById( "surname" ).value,
            "position": document.getElementById( "position" ).value,
            "salary": parseFloat( document.getElementById( "salary" ).value )
        } );

        fetch( "https://localhost:44366/api/employee", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: input

        } ).then( positiveResponse );
    }
}

function positiveResponse ( response ) {
    if ( response.ok ) {
        loadData();
        resetForm();
    }
}

function selectRowForEditorDelete () {
    const table = document.getElementById( "employee-table" );
    const rows = table.getElementsByTagName( "tr" );
    for ( i = 0; i < rows.length; i++ ) {
        const currentRow = table.rows[i];
        const createClickHandler = function ( row ) {
            return function () {
                document.getElementById( "id" ).value = row.getElementsByTagName( "td" )[0].textContent;
                document.getElementById( "name" ).value = row.getElementsByTagName( "td" )[1].textContent;
                document.getElementById( "surname" ).value = row.getElementsByTagName( "td" )[2].textContent;
                document.getElementById( "position" ).value = row.getElementsByTagName( "td" )[3].textContent;
                document.getElementById( "salary" ).value = row.getElementsByTagName( "td" )[4].textContent;

                const id = row.getElementsByTagName( "td" )[0].textContent;
            };
        };
        currentRow.onclick = createClickHandler( currentRow );
    }
}

function editEmployee () {
    const formEmp = document.getElementById( "employee-data" );
    formEmp.onsubmit = ( e ) => {
        e.preventDefault();
        const id = document.getElementById( "id" ).value;
        const input = JSON.stringify( {
            "name": document.getElementById( "name" ).value,
            "surname": document.getElementById( "surname" ).value,
            "position": document.getElementById( "position" ).value,
            "salary": parseFloat( document.getElementById( "salary" ).value )
        } );

        fetch( "https://localhost:44366/api/employee/" + id, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json"
            },
            body: input

        } ).then( positiveResponse );
    }
}

function deleteEmployee() {
    const formEmp = document.getElementById( "employee-data" );
    formEmp.onsubmit = ( e ) => {
        e.preventDefault();
        const id = document.getElementById( "id" ).value;

        fetch( "https://localhost:44366/api/employee/" + id, {
            method: "DELETE",
            headers: {
                "Content-Type": "application/json"
            },
        } ).then( positiveResponse );
    }
}

function resetForm () {
    document.getElementById( "id" ).value = "";
    document.getElementById( "name" ).value = "";
    document.getElementById( "surname" ).value = "";
    document.getElementById( "position" ).value = "";
    document.getElementById( "salary" ).value = "";
}