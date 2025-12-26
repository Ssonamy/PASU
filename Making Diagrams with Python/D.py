import re
import matplotlib.pyplot as plt


path = r"C:\Users\Ssonamy\source\repos\Ssonamy\PASU\PASU-5\Lab_5\Lab_5\bin\Debug\net10.0\log_20251223_114051.csv"

with open(path, "r", encoding="utf-8") as f:
    lines = f.readlines()

# Приоритеты
priority_line = lines[1]
thread_priorities = {}
if "# thread_priorities:" in priority_line:
    for match in re.finditer(r"thread_(\d+)=([A-Za-z]+)", priority_line):
        tid = int(match.group(1))
        prio = match.group(2)
        thread_priorities[tid] = prio
data = []
for line in lines[2:]:
    line = line.strip()
    if not line or line.startswith("#"):
        continue
    # Регулярное выражение для извлечения: time (с запятой), thread_id, iterations
    match = re.match(r'^(\d+,\d+|\d+),\s*(\d+),\s*(\d+)$', line)
    if match:
        time_str = match.group(1).replace(',', '.')
        thread_id = int(match.group(2))
        iters = int(match.group(3))
        data.append((float(time_str), thread_id, iters))
    else:
        print(f"Пропущена строка: '{line}'")

threads = {}
for t, tid, it in data:
    threads.setdefault(tid, []).append((t, it))

plt.figure(figsize=(10, 6))
for tid in sorted(threads):
    pts = sorted(threads[tid])  # сортировка по времени на случай хаоса
    times, iters = zip(*pts)
    label = f"thread_{tid}"
    if tid in thread_priorities:
        label += f" ({thread_priorities[tid]})"
    else:
        label += " (?)"
    plt.plot(times, iters, marker='o', label=label, linewidth=2)

plt.xlabel("Время, сек")
plt.ylabel("Выполнено итераций")
plt.title("Итерации по потокам во времени")
plt.grid(True, linestyle='--', alpha=0.6)
plt.legend()
plt.tight_layout()
plt.show()