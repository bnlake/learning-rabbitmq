import Worker from "../models/Worker";

export interface Props {
	worker: Worker;
}

const WorkerComponent: React.FC<Props> = ({ worker }) => {
	return (
		<div className="card">
			<h2>Worker</h2>
			<p>Id: {worker.id}</p>
			<p>Name: {worker.name}</p>
		</div>
	);
};

export default WorkerComponent;
