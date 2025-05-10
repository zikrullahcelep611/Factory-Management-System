function deleteStorage(storageId) {
    if (confirm("Are you sure you want to delete this storage?")) {
        fetch(`/Storage/Delete/${storageId}`, {
            method: "DELETE"
        })
            .then(response => {
                if (response.ok) {
                    alert("Storage deleted successfully.");
                    location.reload();
                } else {
                    alert("An error occurred while deleting the storage.");
                }
            })
            .catch(error => console.error("Error:", error));
    }
}