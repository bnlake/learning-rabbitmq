import Worker from "../models/Worker";
import WorkerStateButton from "./WorkerStateButton";
export interface Props {
	worker: Worker;
}

const WorkerComponent: React.FC<Props> = ({ worker }) => {
	return (
		<div className="card">
			<div>
				<h2>Worker</h2>
				<p>Id: {worker.id}</p>
				<p>Name: {worker.name}</p>
			</div>
			<div>
				<WorkerStateButton worker={worker} />
			</div>
		</div>
	);
};

export default WorkerComponent;
