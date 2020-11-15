$(document).ready(function () {
	$('#AddOrgForm').validate({
		errorClass: 'help-block animation-slideDown', // You can change the animation class for a different entrance animation - check animations page
		errorElement: 'div',
		errorPlacement: function (error, e) {
			e.parents('.form-group > div').append(error);
		},
		highlight: function (e) {
			$(e).closest('.form-group').removeClass('has-success has-error').addClass('has-error');
			$(e).closest('.help-block').remove();
		},
		success: function (e) {
			e.closest('.form-group').removeClass('has-success has-error');
			e.closest('.help-block').remove();
		},
		rules: {
			'OrgName': {
				required: true,
			},
			'OrgAddress': {
				required: true,
			},
			'DomainId': {
				required: true,
			},
			'OrgTypeId': {
				required: true,
			},
			'OrgBrandId': {
				required: true,
			},
		},
		messages: {
			'OrgName': 'Please enter organisation name',
			'OrgAddress': 'Please enter Organisation address',
			'DomainId': 'Please select organisation domain',
			'OrgTypeId': 'Please select Organisation type',
			'OrgBrandId': 'Please select Organisation brand',
		}
	});
});

$(document).ready(function () {
	$('#AddStaffForm').validate({
		errorClass: 'help-block animation-slideDown', // You can change the animation class for a different entrance animation - check animations page
		errorElement: 'div',
		errorPlacement: function (error, e) {
			e.parents('.form-group > div').append(error);
		},
		highlight: function (e) {
			$(e).closest('.form-group').removeClass('has-success has-error').addClass('has-error');
			$(e).closest('.help-block').remove();
		},
		success: function (e) {
			e.closest('.form-group').removeClass('has-success has-error');
			e.closest('.help-block').remove();
		},
		rules: {
			'FirstName': {
				required: true,
			},
			'LastName': {
				required: true,
			},
			'Email': {
				required: true,
			}
		},
		messages: {
			'FirstName': 'Please enter first name',
			'LastName': 'Please enter last name',
			'Email': 'Please enter email'
		}
	});
});

$(document).ready(function () {
	$('#AddStudentForm').validate({
		errorClass: 'help-block animation-slideDown', // You can change the animation class for a different entrance animation - check animations page
		errorElement: 'div',
		errorPlacement: function (error, e) {
			e.parents('.form-group > div').append(error);
		},
		highlight: function (e) {
			$(e).closest('.form-group').removeClass('has-success has-error').addClass('has-error');
			$(e).closest('.help-block').remove();
		},
		success: function (e) {
			e.closest('.form-group').removeClass('has-success has-error');
			e.closest('.help-block').remove();
		},
		rules: {
			'FirstName': {
				required: true,
			},
			'LastName': {
				required: true,
			},
			'ClassId': {
				required: true,
			},
			'GenderId': {
				required: true,
			},
			'ReligionId': {
				required: true,
			},
			'TribeId': {
				required: true,
			},
			'DateOfBirth': {
				required: true,
			}
		},
		messages: {
			'FirstName': 'Please enter first name',
			'LastName': 'Please enter last name',
			'ClassId': 'Please select class',
			'GenderId': 'Please select gender',
			'ReligionId': 'Please select religion',
			'TribeId': 'Please select tribe',
			'DateOfBirth': 'Please enter Date of birth'
		}
	});
});







