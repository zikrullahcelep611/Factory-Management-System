function deleteBuilding(buildingId) {
    if (confirm("Are you sure you want to delete this building?")) {
        fetch(`/building/delete/${buildingId}`, {
            method: 'DELETE',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        })
            .then(response => {
                if (response.ok) {
                    // Eğer silme işlemi başarılıysa, sayfayı yenile
                    window.location.reload();
                } else {
                    alert("An error occurred while deleting the building.");
                }
            })
            .catch(error => {
                console.error("Error:", error);
                alert("An unexpected error occurred.");
            });
    }
}