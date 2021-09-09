export default (): void => {
    const indeterminateCheckboxes = document.querySelectorAll('.a-checkbox.a-checkbox--indeterminate');

    [...indeterminateCheckboxes].forEach(checkbox => {
        if (checkbox.querySelector('input')) {
            checkbox.querySelector('input').indeterminate = true;
        }
    })
}