// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function loadDepartments() {
    $.ajax({
        url: '/Departments/DepartmentsByCompany',
        method: 'GET',
        data: { companyId: $('#companiesSelect option:selected').val() },
        success: function (data) {
            if (data) {
                $('#departmentsSelect').empty();
                data.forEach(function (a) {
                    $('#departmentsSelect').append('<option value=' + a.value + '>' + a.text + '</option>');
                })
            }
        }
    })
}

function loadEmployees() {
    $.ajax({
        url: '/Employees/EmployeesByDepartment',
        method: 'GET',
        data: { departmentId: $('#departmentsSelect option:selected').val() },
        success: function (data) {
            if (data) {
                $('#EmployeeId').empty();
                data.forEach(function (a) {
                    $('#EmployeeId').append('<option value=' + a.value + '>' + a.text + '</option>');
                })
            }
        }
    })
}


function loadWorkplaces() {
    $.ajax({
        url: '/Workplaces/WorkplacesByCompany',
        method: 'GET',
        data: { companyId: $('#companiesSelect option:selected').val() },
        success: function (data) {
            if (data) {
                $('#WorkplaceId').empty();
                data.forEach(function (a) {
                    $('#WorkplaceId').append('<option value=' + a.value + '>' + a.text + '</option>');
                })
            }
        }
    })
}
