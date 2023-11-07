$(document).ready(function () {
    // When the document is ready, call the GetEmployees function
    GetEmployees();
})
/* Read Data */
function GetEmployees() {
    // This function retrieves employee data via an AJAX request
    $.ajax({
        url: '/employee/GetEmployees', // URL for the AJAX request
        type: 'get', // HTTP method used (GET request)
        datatype: 'json', // Expected data type in response (JSON)
        contentType: 'application/json;charset=utf-8', // Content type of the request
        success: function (response) {
            // This function is called when the AJAX request is successful
            if (response == null || response == undefined || response.length == 0) {
                // Handle case where no employees are available
                var object = '';
                object += '<tr>';
                object += '<td colspan="6">' + 'Employees not available' + '</td>';
                object += '</tr>';
                $('#tblBody').html(object);
            } else {
                // Process the retrieved employee data
                var object = '';
                let i = 1;
                $.each(response, function (index, item) {
                    // Build HTML rows for each employee
                    object += '<tr>';
                    object += '<td>' + i++ + '</td>';
                    object += '<td>' + item.firstName + '</td>';
                    object += '<td>' + item.lastName + '</td>';
                    object += '<td>' + item.age + '</td>';
                    object += '<td>' + item.email + '</td>';
                    object += '<td> <a href="#" class="btn btn-primary btn-sm" onclick="Edit(' + item.id +
                        ')">Edit</a> <a href="#" class="btn btn-danger btn-sm" onclick="Delete(' + item.id +
                        ')">Delete</a></td>';
                    object += "</tr>"
                });
                // Insert the generated HTML into the table body
                $('#tblBody').html(object);
            }
        },
        error: function () {
            // This function is called if the AJAX request encounters an error
            alert('Unable to read the data.');
        }
    })
}

// Function to handle the click event of the "Add" button
$('#btnAdd').click(function () {
    $('#EmployeeModal').modal('show');
    $('#modalTitle').text('Add Employee');
});

/*  Insert Data  */
function Insert() {
    // Function to insert employee data
    var result = Validate();

    if (result == false) {
        return false;
    }

    // Create an object with employee data from the form
    var formData = new Object();
    formData.id = $('#Id').val();
    formData.firstName = $('#FirstName').val();
    formData.lastName = $('#LastName').val();
    formData.email = $('#Email').val();
    formData.age = $('#Age').val();

    // Send a POST request to insert the employee data
    $.ajax({
        url: '/employee/Insert',
        data: formData,
        type: 'post',
        success: function (response) {
            // Handle the response after the insert operation
            if (response == null || response == undefined || response.length == 0) {
                alert("Unable to save the data.");
            } else {
                GetEmployees();
                HideModal();
                alert(response);
            }
        },
        error: function () {
            alert("Unable to save the data.");
        }
    });
}

// Function to hide the modal and clear form data
function HideModal() {
    ClearData();
    $('#EmployeeModal').modal('hide');
}

// Function to clear form data and reset border colors
function ClearData() {
    // ... (code to clear form fields and reset border colors)
    $('#FirstName').val('');
    $('#LastName').val('');
    $('#Email').val('');
    $('#Age').val('');

    $('#FirstName').css('border-color', 'lightgrey');
    $('#LastName').css('border-color', 'lightgrey');
    $('#Age').css('border-color', 'lightgrey');
    $('#Email').css('border-color', 'lightgrey');
}

// Function to validate form input
function Validate() {
    var isValid = true;

    if ($('#FirstName').val().trim() == "") {
        $('#FirstName').css('border-color', 'Red');
        isValid = false;
    } else {
        $('#FirstName').css('border-color', 'lightgrey');
    }

    if ($('#LastName').val().trim() == "") {
        $('#LastName').css('border-color', 'Red');
        isValid = false;
    } else {
        $('#LastName').css('border-color', 'lightgrey');
    }

    if ($('#Age').val().trim() == "") {
        $('#Age').css('border-color', 'Red');
        isValid = false;
    } else {
        $('#Age').css('border-color', 'lightgrey');
    }

    if ($('#Email').val().trim() == "") {
        $('#Email').css('border-color', 'Red');
        isValid = false;
    } else {
        $('#Email').css('border-color', 'lightgrey');
    }

    return isValid;
}

// Event handlers for form fields' change events
$('#FirstName').change(function () {
    Validate();
})
$('#LastName').change(function () {
    Validate();
})
$('#Email').change(function () {
    Validate();
})
$('#Age').change(function () {
    Validate();
})

// Function to handle the "Edit" button click event
function Edit(id) {
    $.ajax({
        url: '/employee/Edit?id=' + id,
        type: 'get',
        contentType: 'application/json;charset=utf-8',
        dataType: 'json',
        success: function (response) {
            if (response == null || response == undefined) {
                alert('Unable to read the data.');
            }
            else if (response.length == 0) {
                alert('Data not available with the id ' + id);
            } else {
                $('#EmployeeModal').modal('show');
                $('#modalTitle').text('Update Employee');
                $('#Save').css('display', 'none');
                $('#Update').css('display', 'block');
                $('#Id').val(response.id);
                $('#FirstName').val(response.firstName);
                $('#LastName').val(response.lastName);
                $('#Email').val(response.email);
                $('#Age').val(response.age);
            }
        },
        error: function () {
            alert('Unable to read the data.')
        }
    });
}

/* Edit */
function Update() {
    var result = Validate();
    if (result == false) {
        return false;
    }

    var formData = new Object();
    formData.id = $('#Id').val();
    formData.firstName = $('#FirstName').val();
    formData.lastName = $('#LastName').val();
    formData.email = $('#Email').val();
    formData.age = $('#Age').val();

    $.ajax({
        url: '/employee/Update',
        data: formData,
        type: 'put',
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                alert("Unable to save the data.Fail");
            } else {
                HideModal();
                GetEmployees();
                alert("Employee Deleted Successfully!");
            }
        },
        error: function () {
            alert("Error Occured");
        }
    });
}

/*Delete Employee*/
function Delete(id) {
    if (confirm('Are you sure to delete this employee data?')) {
        $.ajax({
            url: '/employee/Delete?id=' + id,
            type: 'post',
            success: function (response) {
                if (response == null || response == undefined || response.length == 0) {
                    alert('Unable to delete the data.');
                }
                else {
                    GetEmployees();
                    alert('Employee Deleted Successfully!');
                }
            },
            error: function () {
                alert('Error Occured!')
            }
        });
    }
}