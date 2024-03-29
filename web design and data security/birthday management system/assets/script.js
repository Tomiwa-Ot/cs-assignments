/**
 * Add or update GET variable in the current URL
 */
function addFilterToURL(key, value) {
    var destination = null
    var url = window.location.href;
    var pattern = new RegExp('(' + key + '=).*?(&|$)');

    if (value.length == 0) {
        // Remove the GET variable if an empty value is passed
        destination = url.replace(new RegExp('([?&])' + key + '=[^&]*(&|$)'), '$1').replace('/[&?]$/', '');
    } else if (url.search(pattern) >= 0) {
        // If the parameter exists, replace its value
        destination = url.replace(pattern, '$1' + value + '$2');
    } else {
        // Add parameter to the URL since it doesn't exist
        if (url.indexOf('?') > 0)
            destination = url + '&' + key + '=' + value;
        else
            destination = url + '?' + key + '=' + value;
    }

    // Navigate to the new URL
    if (destination !== null)
        window.location.href = destination;
}

// Event listeners for when a filter is selected
var filters = document.getElementsByClassName('filter');
for (var filter of filters) {
    if (filter.id === 'gender') {
        filter.addEventListener('change', function() {
            addFilterToURL('gender', this.value);
        });
    } else if (filter.id === 'department') {
        filter.addEventListener('change', function() {
            addFilterToURL('department', this.value);
        });
    } else if (filter.id === 'staff_type') {
        filter.addEventListener('change', function() {
            addFilterToURL('staff_type', this.value);
        });
    } else if (filter.id === 'month') {
        filter.addEventListener('change', function() {
            addFilterToURL('month', this.value);
        });
    }        
}

// Clear filters by removing all GET variables
var clearFiltersBtn = document.getElementById('clear-filter');
if (clearFiltersBtn !== null) {
    clearFiltersBtn.addEventListener('click', function() {
        window.location.href = window.location.href.split('?')[0];
    });
}

// Get the modal element
var modal = document.getElementById("registration-modal");
// Get the button that opens the modal
var openRegistrationForm = document.getElementById("open-modal");
// Get the <span> element that closes the modal
var closeRegistrationForm = document.getElementsByClassName("close")[0];

openRegistrationForm.addEventListener('click', function() {
    modal.style.display = "block";
});

closeRegistrationForm.addEventListener('click', function() {
    modal.style.display = "none";
});

window.addEventListener('click', function(event) {
    if (event.target == modal)
        modal.style.display = "none";
});