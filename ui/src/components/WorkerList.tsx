import WorkerComponent from "./Worker";
import Worker from "../models/Worker";

function WorkerList() {
	const workers: Array<Worker> = [
		new Worker("Heart Burn"),
		new Worker("Headache"),
		new Worker("Stress"),
	];

	return (
		<div>
			<h1>Workers</h1>
			<ul>
				{workers.map((worker) => (
					<li>
						<WorkerComponent worker={worker} />
					</li>
				))}
			</ul>
		</div>
	);
}

export default WorkerList;
