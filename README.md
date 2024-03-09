# Visus.VcpkgStatus

## Installation
Publish and deploy the application:

```bash
mkdir /var/www/vcpkgstatus
\cp -r /tmp/publish/* /var/www/vcpkgstatus
chown -R nginx:nginx *
chmod u+x /var/www/vcpkgstatus/Visus.VcpkgStatus
```

Create a service file `/etc/systemd/system/kestrel-vcpkgstatus.service` like this:

```ini
[Unit]
Description=Vcpkg Badge Service

[Service]
WorkingDirectory=/var/www/vcpkgstatus
ExecStart=/var/www/vcpkgstatus/Visus.VcpkgStatus
Restart=always
RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=kestrel-vcpkgstatus
User=nginx
Environment=ASPNETCORE_ENVIRONMENT=Production 

[Install]
WantedBy=multi-user.target
```

Register and start the service:

```bash
systemctl daemon-reload
systemctl enable kestrel-vcpkgstatus.service
systemctl start kestrel-vcpkgstatus.service
systemctl status kestrel-vcpkgstatus.service
```
