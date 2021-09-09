// Add randomly between 2 to 10% progress.
function setProgress(container: Element): void {
  let currentValue: number = Number(container.getAttribute('data-progress'));
  if(currentValue === 100) {
    currentValue = 0;
  }

  const progress: number = Math.floor(Math.random() * (10 - 2) + 5);
  let newValue: number = currentValue + progress;

  if (newValue > 100) {
    newValue = 100;
  }
  container.setAttribute('data-progress', `${newValue}`);
}

export default (): void => {
  const examples = document.querySelectorAll('#random-progress');
  examples.forEach(container => {
    window.setInterval(setProgress.bind(this, container), 1000);
  });
};
